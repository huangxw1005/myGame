--[[
    游戏总入口，自更新完成后加载
    加载各个游戏模块（network, manager, 所有面板等）
]]
json = require "cjson"
inspect = require "core/inspect"

-- manager
lanMgr = LanguageManager.GetInstance()
sceneMgr = SceneManager.GetInstance()
guiMgr = GUIManager.Instance
resMgr = ResourceManager.Instance
networkMgr = NetworkManager.Instance
chMgr = CharacterManager.Instance

require "config"
require "network/network"
require "core/fsm"
require "core/mvc"
require "notifyname"

require "battle/battle"

-- scene
require "scene/SceneMain"
require "scene/SceneLoading"
require "scene/SceneBattle"
require "scene/SceneCity"

-- register windows
facade = Facade:getInstance()
facade:registerCommand(OPEN_WINDOW, require("controller/open_win_cmd").new())
facade:registerCommand(WAIT, require("controller/wait_cmd").new())
facade:registerCommand(TIP, require("controller/tip_cmd").new())

-- model
facade:registerProxy(require("model/user_proxy").new())
facade:registerProxy(require("model/bag_proxy").new())

-- view
facade:registerMediator(require("window/PanelLogin"))
facade:registerMediator(require("window/PanelMenu"))
facade:registerMediator(require("window/PanelBag"))
facade:registerMediator(require("window/PanelEquip"))
facade:registerMediator(require("window/PanelAlert"))
facade:registerMediator(require("window/PanelItemDetail"))
facade:registerMediator(require("window/PanelWait"))
facade:registerMediator(require("window/PanelTip"))
facade:registerMediator(require("window/PanelMain"))
facade:registerMediator(require("window/PanelBattle"))
facade:registerMediator(require("window/PanelGift"))
facade:registerMediator(require("window/PanelBattleBalance"))

-- cfg
CFG = {}

--管理器--
Game = {}
local this = Game

local transform
local gameObject
local WWW = UnityEngine.WWW


fontArial = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 1);

--初始化完成(自更新)
function Game.OnInitOK()
    print('Game Init OK ...')

    math.randomseed(os.time())
    
    networkMgr:OnInit()
    networkMgr:SendConnect(CONFIG_SOCKET_IP, CONFIG_SOCKET_PORT)

    local data = resMgr:LoadAsset('Cfg/item'):ToString()
    CFG.items = json.decode(data)

    local data = resMgr:LoadAsset('Cfg/equip'):ToString()
    CFG.equips = json.decode(data)

    local data = resMgr:LoadAsset('Cfg/skill'):ToString()
    CFG.skills = json.decode(data)

    facade:sendNotification(OPEN_WINDOW, {name="PanelLogin"})
end

--销毁--
function Game.OnDestroy()
end
