local gameObject
local transform

PanelMenu = {}
local this = PanelMenu
this._name = "PanelMenu"

-- --------------------------------------------------------------------
--	c# callback
-----------------------------------------------------------------------
function PanelMenu.Awake(obj)
	gameObject = obj
	transform = obj.transform

	this.InitPanel()
end
function PanelMenu.OnDestroy()
end

-- --------------------------------------------------------------------
--	mvc notication
-----------------------------------------------------------------------
function PanelMenu:listNotificationInterests()
	return {}
end
function PanelMenu:handleNotification(notification)
end

--初始化面板--
function PanelMenu.InitPanel()

    local userProxy = facade:retrieveProxy("UserProxy")
	local text_coin = transform:Find("Text Coin"):GetComponent('Text')
	text_coin.text = userProxy.Coin

	local btnBag = transform:Find("Button Bag").gameObject
	local btnEquip = transform:Find("Button Equip").gameObject
	local btnBattle = transform:Find("Button Battle").gameObject
	local btnShop = transform:Find("Button Shop").gameObject
	local btnGift = transform:Find("Button Gift").gameObject

	local window = transform:GetComponent('LuaBehaviour')

	window:AddClick(btnBag, this.OnBtnBag)
	window:AddClick(btnEquip, this.OnBtnEquip)
	window:AddClick(btnBattle, this.OnBtnBattle)
	window:AddClick(btnShop, this.OnBtnShop)
	window:AddClick(btnGift, this.OnBtnGift)
	
end

--单击事件--
function PanelMenu.OnBtnBag(go)
	print('OnBtnBag')
    facade:sendNotification(OPEN_WINDOW, {name="PanelBag"})
end

function PanelMenu.OnBtnEquip(go)
	print('OnBtnEquip')
    sceneMgr:GotoScene(SceneID.City)
end

function PanelMenu.OnBtnBattle(go)
	print('OnBtnBattle')
    sceneMgr:GotoScene(SceneID.Battle)
end

function PanelMenu.OnBtnShop(go)
	print('OnBtnShop')
    facade:sendNotification(WAIT, {name="show"})
end

function PanelMenu.OnBtnGift(go)
	print('OnBtnGift')
    facade:sendNotification(OPEN_WINDOW, {name="PanelGift"})
end

return PanelMenu