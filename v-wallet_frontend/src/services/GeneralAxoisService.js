import axios from "axios";
const user = JSON.parse(localStorage.getItem("User"));
var token = ""
if(user != null)
    token = user.token

var baseUrl = "http://localhost:5093/api/v1/Users"
class GeneralAxoisService {
    async getMethod(url){
        return await axios.get(baseUrl + url, { headers: { "Authorization" : "Bearer " + token }});
    }

    async postMethod(url, obj){
        return await axios.post(baseUrl + url, obj, { headers: { "Authorization" : "Bearer " + token }})
    }

    async putMethod(url){
        return await axios.put(baseUrl + url, { headers: { "Authorization" : "Bearer " + token }})
    }

    async deleteMethod(url){
        return await axios.delete(baseUrl + url, { headers: { "Authorization" : "Bearer " + token }})
    }

    async getDefaultCryptos(){
        var btcWorth = 0
        var ethWorth = 0
        var dogeWorth = 0
        var tetherWorth = 0
        
        await axios.get("https://api.coingecko.com/api/v3/coins/bitcoin")
        .then(res => {
            btcWorth = res.data.market_data.current_price.usd
        })

        
        await axios.get("https://api.coingecko.com/api/v3/coins/ethereum")
        .then(res => {
            ethWorth = res.data.market_data.current_price.usd
        })

        await axios.get("https://api.coingecko.com/api/v3/coins/dogecoin")
        .then(res => {
            dogeWorth = res.data.market_data.current_price.usd
        })

        await axios.get("https://api.coingecko.com/api/v3/coins/tether")
        .then(res => {
            tetherWorth = res.data.market_data.current_price.usd
        })
        
        return [
            {name: "Bitcoin", worthUSD: btcWorth, img: "https://toppng.com/uploads/preview/bitcoin-png-bitcoin-logo-transparent-background-11562933997uxok6gcqjp.png"},
            {name: "Ethereum", worthUSD: ethWorth, img: "https://pngset.com/images/ethereum-eth-icon-ethereum-triangle-transparent-png-1334966.png"},
            {name: "Dogecoin", worthUSD: dogeWorth, img: "https://e7.pngegg.com/pngimages/140/487/png-clipart-dogecoin-cryptocurrency-digital-currency-doge-mammal-cat-like-mammal.png"},
            {name: "Tether", worthUSD: tetherWorth, img: "https://icons.iconarchive.com/icons/cjdowner/cryptocurrency-flat/1024/Tether-USDT-icon.png"}
        ]
    }
}

export default new GeneralAxoisService()