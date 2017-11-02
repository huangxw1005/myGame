local gameObject
local transform

PanelMain = {}
local this = PanelMain
this._name = 'PanelMain'

-- --------------------------------------------------------------------
--	c# callback
-----------------------------------------------------------------------
function PanelMain.Awake(obj)
	gameObject = obj
	transform = obj.transform

	this.InitPanel()
end

function PanelMain.OnDestroy()
	gameObject = nil
end

-- --------------------------------------------------------------------
--	mvc notication
-----------------------------------------------------------------------
function PanelMain:listNotificationInterests()
	return {}
end
function PanelMain:handleNotification(notification)
end


--初始化面板--
function PanelMain.InitPanel()

    local userProxy = facade:retrieveProxy("UserProxy")

    local rtt = transform:Find("CameraRTT")
    local animator = rtt:GetComponentInChildren(typeof(UnityEngine.Animator))
    animator:Play('run')
	
end
return PanelMain