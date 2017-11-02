local Sequence = DG.Tweening.Sequence
local Tweener = DG.Tweening.Tweener
local DOTween = DG.Tweening.DOTween
local gameObject
local transform

PanelWait = {}
local this = PanelWait
this._name = 'PanelWait'

-- --------------------------------------------------------------------
--	c# callback
-----------------------------------------------------------------------
function PanelWait.Awake(obj)
	gameObject = obj
	transform = obj.transform
end

function PanelWait.OnDestroy()
	gameObject = nil
	this.sequence = nil
	this.rot = nil
end

-- --------------------------------------------------------------------
--	mvc notication
-----------------------------------------------------------------------
function PanelWait:listNotificationInterests()
	return {}
end
function PanelWait:handleNotification(notification)
end

function PanelWait:show()
	if gameObject then
		gameObject:SetActive(true)
	else 
    	facade:sendNotification(OPEN_WINDOW, {name="PanelWait"})
	end

	if this.rot then
		this.rot:Restart(true)
	else 
		local img = transform:Find("Image")
		this.rot = img:DORotate(Vector3.New(0, 0, -360), 1, DG.Tweening.RotateMode.FastBeyond360)
		this.rot:SetEase(DG.Tweening.Ease.Linear)
		this.rot:SetLoops(-1)
	end

	-- 10s timer
	this.sequence = DOTween.Sequence()
	this.sequence:AppendInterval(10)
	this.sequence:AppendCallback(DG.Tweening.TweenCallback(function ()
		this.hide()
		
    	facade:sendNotification(OPEN_WINDOW, {name="PanelAlert", data={lanMgr:GetValue('TITLE_TIP'), lanMgr:GetValue('NETWORK_TIMEOUT')}})
	end))
	this.sequence:Play()

end

function PanelWait:hide()
	if gameObject then gameObject:SetActive(false) end
	if this.sequence then this.sequence:Kill(true) end
end

return PanelWait