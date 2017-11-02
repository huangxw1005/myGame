--[[
	战斗管理器
--]]
require "battle/enemy_mgr"
require "battle/effect_mgr"

local GameObject = UnityEngine.GameObject
local Sequence = DG.Tweening.Sequence
local Tweener = DG.Tweening.Tweener
local DOTween = DG.Tweening.DOTween

battle = {}
local TAG = "battle"
local this = battle

local gameObject
local transform
local userProxy

function battle.init(obj)
	gameObject = obj
	transform = obj.transform

    userProxy = facade:retrieveProxy("UserProxy")

	this.canvas_bot = GameObject.Find("Canvas Bot").transform
	this.canvas_hud = GameObject.Find("Canvas HUD").transform

	local go_camera_ui = GameObject.Find("UI Camera")
	this.camera_ui = go_camera_ui:GetComponent('Camera')
	local go_camera = GameObject.Find("Main Camera")
	this.camera = go_camera:GetComponent('Camera')
	local lockview = go_camera:GetComponent('LockViewCameraController')

	this.UID = 0
	this.player = nil
	this.player_tf = nil

	this.init_player()

	lockview:SetTarget(this.player_tf)

	-- object cache
	ObjectPool.CreatePool('HealthBar', resMgr:LoadAsset('UI/Widget/HealthBar'), 1)
	ObjectPool.CreatePool("CritNum", resMgr:LoadAsset('UI/Widget/CritNum'), 1)
	ObjectPool.CreatePool("HpAddNum", resMgr:LoadAsset('UI/Widget/HpAddNum'), 1)
	ObjectPool.CreatePool("Coin", resMgr:LoadAsset('Prefabs/Item/coin'), 5)

	-- enemy
	enemy_mgr.init()

	this.dropitems = {}
end

function battle.init_player()

	this.player = chMgr:AddPlayer(7, 6, 21)
	this.skills = {100, 104, 102}
	for i = 1, #this.skills do
		this.player:AddSkill(this.skills[i])
	end

	this.player_tf = this.player.transform
end

-- enmey 生成
function battle.enemy_spawn(enemy)
	enemy_mgr.spawn(enemy)
end

function battle.camera_shake()
	local shake = this.camera:DOShakePosition(0.3, 0.2, 20, 90)
end

function battle.add_hp(actor, value)
	print("add_hp", id, value)
	local pos = actor.transform.position + Vector3.New(0, 0, 0)
	effect_mgr.create_hp_label(pos, value)

    facade:sendNotification(BATTLE_HP_CHANGE, {hp=actor.HP, hpmax=actor.HPMax})
end

function battle.add_sp(actor, value)
	print("add_sp", id, value)
	local pos = actor.transform.position + Vector3.New(0, 0, 0)
	effect_mgr.create_hp_label(pos, value)

    facade:sendNotification(BATTLE_MP_CHANGE, {mp=actor.MP, mpmax=actor.MPMax})
end

function battle.hp_change(value, max)
    facade:sendNotification(BATTLE_HP_CHANGE, {hp=value, hpmax=max})
end
function battle.sp_change(value, max)
    facade:sendNotification(BATTLE_MP_CHANGE, {mp=value, mpmax=max})
end

function battle.show_tip(str)
	facade:sendNotification(TIP, {data={lanMgr:GetValue(str)}})
end

function battle.cast_skill(idx)
	local idx = idx + 1
	facade:sendNotification(BATTLE_CAST_SKILL, {idx=idx, id=this.skills[idx]})
end

function battle.player_hit(id, attackid)
	print("player_hit", id, attackid)

	if this.player.HP == 0 then return end

	-- calculate attack
	local attack = math.random(1, 10)
	local hp = math.max(0, this.player.HP - attack)
	this.player.HP = hp

	if hp == 0 then 
		-- die, balance
		this.player:ActDie()

		local function balance( ... )
			facade:sendNotification(OPEN_WINDOW, {name="PanelBattleBalance"})
		end
		local timer = Timer.New(balance, 2, 0, true)
		timer:Start()
	end

    facade:sendNotification(BATTLE_HP_CHANGE, {hp=hp})

	-- effect
	local pos = this.player_tf.position + Vector3.New(0, 1, 0)
	effect_mgr.create_hit_label(pos, -attack)
	
	this.camera_shake()
end

function battle.enemy_hit(id, attackid)
	print("enemy_hit", id, attackid)

	-- calculate attack
	local attack = math.random(10, 30)

	local enemy = enemy_mgr.enemy_hit(id, attack)
	if enemy then
		-- effect
		local pos = enemy[3].position + Vector3.New(0, 1, 0)
		effect_mgr.create_hit_label(pos, -attack)

	end

end

function battle.player_enter_npc(id, npcid)
	print("player_enter_npc", id, npcid)

	if npcid == 0 then
		facade:sendNotification(TIP, {data={lanMgr:GetValue('ITEM_COMPOSE_SUCCESS')}})
	elseif npcid == 1 then
    	facade:sendNotification(OPEN_WINDOW, {name="PanelEquip"})
	elseif npcid == 2 then

	end

end

function battle.player_break(id, pos)
	print("player_break", id, pos)

	this.drop_item(pos + Vector3.New(0, 0.2, 0), this.UID)
	this.UID = this.UID + 1
end

function battle.player_take_item(id, itemid)
	print ('player_take_item', itemid)

	GameObject.Destroy(this.dropitems[itemid])
end


function battle.drop_coin(pos)
	local pos = pos+Vector3.New(0, 0.5, 0)
	local item = ObjectPool.Spawn('Coin', pos).transform
	Util.ChangeLayers(item, 'Item')
	local trailrender = item:GetComponentInChildren(typeof(UnityEngine.TrailRenderer))
	trailrender:Clear()

	local rot = item:DORotate(Vector3.New(0, 720, 0), 1, DG.Tweening.RotateMode.FastBeyond360)
	local move = item:DOMoveY(pos.y+1.5, 1, false)

	local sequence = DOTween.Sequence()
	sequence:Append(rot)
	sequence:Join(move)
	sequence:AppendCallback(DG.Tweening.TweenCallback(function ()
		trailrender:Clear()
		item:SetParent(this.canvas_bot)
		Util.ChangeLayers(item, 'UI')
		local spos = this.camera:WorldToScreenPoint(item.position)
		spos.z = 100
		local wpos = battle.camera_ui:ScreenToWorldPoint(spos)
		item.position = wpos

		local wpos = this.camera_ui:ScreenToWorldPoint(Vector3.New(350, 500, spos.z))
		local move = item:DOMove(wpos, 1, false)
		local sequence = DOTween.Sequence()
		sequence:Append(move)
		sequence:AppendCallback(DG.Tweening.TweenCallback(function ()
			ObjectPool.Recycle(item.gameObject)

			userProxy.Coin = userProxy.Coin + 1
    		facade:sendNotification(COIN_CHANGE, {coin=userProxy.Coin})
		end))
		sequence:SetAutoKill()
		
	end))
	sequence:Play()
	sequence:SetAutoKill()
end


function battle.drop_item(pos, id)
	local prefab = resMgr:LoadAsset('Prefabs/Item/crystal')

    local go = Util.Instantiate(prefab, transform, pos)

	local item = go:GetComponent('DropItem')
	item.ID = id
	this.dropitems[id] = go

	local move = go.transform:DOMoveY(pos.y+0.2, 1, false)	
	local move2 = go.transform:DOMoveY(pos.y, 1, false)	
	local sequence = DOTween.Sequence()
	sequence:Append(move)
	sequence:Append(move2)
	sequence:SetLoops(-1)
	sequence:Play()
	sequence:SetAutoKill()

end

function battle.destroy()
	print('battle.destroy')
	-- body
	chMgr:RemoveAll()

	resMgr:UnloadAsset('UI/Widget/HealthBar')
	resMgr:UnloadAsset('UI/Widget/CritNum')
	resMgr:UnloadAsset('UI/Widget/HpAddNum')
	resMgr:UnloadAsset('Prefabs/Item/coin')

end