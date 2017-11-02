-- 用户数据
local UserProxy = class("UserProxy", IProxy)
local TAG = "UserProxy"

function UserProxy:ctor()
	self._proxyName = "UserProxy"

	-- test
	self.UID = 100
	self.Name = 'AdamWu'
	self.Coin = math.random(100, 2000)
end

-- 解析
function UserProxy:parse(data)
	print(TAG, "parse")

	self.UID = data.id
	self.Name = data.name
	self.Level = data.lv
	self.Exp = data.exp
	self.Coin = data.coin

    
    print(inspect(self, 1))
end

return UserProxy