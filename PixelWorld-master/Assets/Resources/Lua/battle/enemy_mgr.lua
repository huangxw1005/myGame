--[[
	enemy生成、管理、销毁
--]]
local GameObject = UnityEngine.GameObject
local Sequence = DG.Tweening.Sequence
local Tweener = DG.Tweening.Tweener
local DOTween = DG.Tweening.DOTween
local UpdateBeat = UpdateBeat

local enemy_mgr = {}
local TAG = "enemy_mgr"
local this = enemy_mgr

function enemy_mgr.init()

	this.enemys = {}

	-- update
	UpdateBeat:Add(this.Update, this)
end

function enemy_mgr.spawn(enemy)

	local transform = enemy.transform

	local bar = ObjectPool.Spawn('HealthBar', battle.canvas_hud)
	local follow = bar:GetComponent('Follow')
	follow.target = transform
	follow.offset = Vector3.New(0, 1, 0)

	local slider = bar.transform:Find('Slider'):GetComponent('Slider') 
	slider.value = 1

	this.enemys[enemy.ID] = {enemy, enemy.gameObject, transform, bar, slider}
end

function enemy_mgr.Update()
end

function enemy_mgr.get_enemy(id)
	local id = tonumber(id)
	return this.enemys[id]
end


function enemy_mgr.enemy_hit(id, attack)
	local id = tonumber(id)
	local enemy = this.enemys[id]
	if enemy == nil then return nil end
	
	if enemy[1].HP == 0 then return nil end

	local hp = math.max(0, enemy[1].HP - attack)
	enemy[1].HP = hp
	enemy[5].value = hp / 100

	if hp == 0 then 
		-- die, balance
		this.enemy_die(id)

		battle.drop_coin(enemy[3].position)
	end

	return enemy
end

function enemy_mgr.enemy_die(id)
	local id = tonumber(id)
	local enemy = this.enemys[id]
	if enemy == nil then return end


	enemy[1]:ActDie()		
	
	-- remove bar
	ObjectPool.Recycle(enemy[4])

	local renderer = enemy[2]:GetComponentInChildren(typeof(UnityEngine.SkinnedMeshRenderer))
	local mat = renderer.material

	alpha = mat:DOFade(0, 2)
	
	local sequence = DOTween.Sequence()
	sequence:AppendInterval(1)
	sequence:Append(alpha)
	sequence:AppendCallback(DG.Tweening.TweenCallback(function ()
		-- remove
		chMgr:Remove(enemy[1])
		this.enemys[id] = nil
	end))
	sequence:Play()
end


_G['enemy_mgr'] = enemy_mgr

return enemy_mgr