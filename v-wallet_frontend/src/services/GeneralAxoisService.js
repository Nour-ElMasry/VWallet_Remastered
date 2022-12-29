import axios from "axios";

class GeneralAxoisService {
    async getMethod(url){
        return await axios.get(url);
    }

    async postMethod(url, obj){
        return await axios.post(url, obj)
    }

    async updateCard(url, card){
        return await axios.put(url, card)
    }

    async sendMoney(url){
        return await axios.put(url)
    }

    async removeCard(url){
        return await axios.delete(url)
    }

    async getDefaultCryptos(){
        var btcWorth = 0
        var ethWorth = 0
        var dogeWorth = 0
        var tetherWorth = 0
        
        await this.getMethod("https://api.coingecko.com/api/v3/coins/bitcoin")
        .then(res => {
            btcWorth = res.data.market_data.current_price.usd
        })

        
        await this.getMethod("https://api.coingecko.com/api/v3/coins/ethereum")
        .then(res => {
            ethWorth = res.data.market_data.current_price.usd
        })

        await this.getMethod("https://api.coingecko.com/api/v3/coins/dogecoin")
        .then(res => {
            dogeWorth = res.data.market_data.current_price.usd
        })

        await this.getMethod("https://api.coingecko.com/api/v3/coins/tether")
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