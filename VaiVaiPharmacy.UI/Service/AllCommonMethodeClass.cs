using Microsoft.AspNetCore.Http;
using UXComponents;
using VaiVaiPharmacy.UI.Models;
using VaiVaiPharmacyModelClass;
using VaiVaiPharmacyModelClass.sale;
using VaiVaiPharmacyModelClass.Stock;

namespace VaiVaiPharmacy.UI.Service
{
    public class AllCommonMethodeClass
    {
        private readonly IWebHostEnvironment _env;
        private readonly sessionData _session;
        private readonly GenericCRUDOperation _operation;
        public AllCommonMethodeClass(IWebHostEnvironment env, sessionData storage, GenericCRUDOperation operation)
        {
            _env = env;
            _session = storage;
            _operation = operation;
        }

        public async Task<bool> isDuplicateMedicine(MedicineDetails med)
        {
            List<MedicineDetails> _dataList = new List<MedicineDetails>();
            _dataList = await _operation.getAll<MedicineDetails>();

            foreach (MedicineDetails data in _dataList)
            {
                if (data.medicineName == med.medicineName && data.type == med.type && data.stength == med.stength)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<Dropdown>> MedicineDropDown()
        {
            List<Dropdown> _dropList = new List<Dropdown>();
            List<MedicineDetails> _dataList = new List<MedicineDetails>();
            _dataList = await _operation.getAll<MedicineDetails>();

            foreach (MedicineDetails data in _dataList)
            {
                _dropList.Add(new Dropdown
                {
                    text = data.medicineName + "(" + data.stength + ") - " + data.type,
                    value = data.id + ""
                });
            }

            return _dropList;
        }

        public async Task StockAdd(StockBuy _stockBuy, List<StockBuyetails> _stockBuyDetails)
        {
            var stockBuy = await _operation.saveOrUpdateOrDeleteData<StockBuy>(_stockBuy);
            foreach (var item in _stockBuyDetails)
            {
                item.stockId = stockBuy.id;
                var medPrice = await _operation.getInfoById<MedicinePrice>(item.medicineid + "", "medId");
                var stockavl = await _operation.getInfoById<stockAvailable>(item.medicineid + "", "medId");


                decimal avgBuyPrice = (item.actualPrice / item.quentity);
                // medPrice.price = item.sellPrice;
                if (stockavl.remain == 0)
                {
                    medPrice.avgBuyPrice = avgBuyPrice;
                    medPrice.avgSellPrice = item.sellPrice;
                }
                else
                {
                    medPrice.avgBuyPrice = ((medPrice.avgBuyPrice + avgBuyPrice) / 2);
                    medPrice.avgSellPrice = ((medPrice.avgSellPrice + item.sellPrice) / 2);
                }

                stockavl.remain = stockavl.remain + item.quentity;
                await _operation.saveOrUpdateOrDeleteData(item);
                await _operation.saveOrUpdateOrDeleteData(medPrice);
                await _operation.saveOrUpdateOrDeleteData(stockavl);
            }
        }

        public async Task StockDelete(int stockId)
        {
            var stockBuy = await _operation.getInfoById<StockBuy>(stockId + "", "id");
            // delete stock
            await _operation.saveOrUpdateOrDeleteData<StockBuy>(stockBuy, true);

            var temp = await _operation.getAll<StockBuyetails>();
            foreach (var item in temp.Where(x => x.stockId == stockId && x.status != "DEL"))
            {
                await _operation.saveOrUpdateOrDeleteData(item, true);
                var stockAvl = await _operation.getInfoById<stockAvailable>(item.medicineid + "", "medId");
                stockAvl.remain = stockAvl.remain - item.quentity;
                await _operation.saveOrUpdateOrDeleteData(stockAvl);
                await updatePriceAndStock(item.medicineid);
            }
        }

        public async Task StockIndividualDelete(int stockId, StockBuyetails medBuy)
        {
            var stockBuy = await _operation.getInfoById<StockBuy>(stockId + "", "id");
            var temp = await _operation.getAll<StockBuyetails>();
            var temp1 = temp.Where(x => x.stockId == stockId && x.status != "DEL");
            // delete stock
            if(temp1.Count() == 1)
            {
                await _operation.saveOrUpdateOrDeleteData<StockBuy>(stockBuy,true);
            }
            else
            {
                await _operation.saveOrUpdateOrDeleteData<StockBuy>(stockBuy);
            }
            await _operation.saveOrUpdateOrDeleteData(medBuy, true);
            var stockAvl = await _operation.getInfoById<stockAvailable>(medBuy.medicineid + "", "medId");
            stockAvl.remain = stockAvl.remain - medBuy.quentity;
            await _operation.saveOrUpdateOrDeleteData(stockAvl);
            await updatePriceAndStock(medBuy.medicineid);
        }

        private async Task updatePriceAndStock(int medid)
        {
            var priceDetails = await _operation.getInfoById<MedicinePrice>(medid + "", "medId");
            var stockBuyDetails = await _operation.getAll<StockBuyetails>();
            var temp = stockBuyDetails.Where(x => x.medicineid == medid && x.status != "DEL" && x.status != "EMPTY");
            decimal buyprice = 0, sellprice = 0, quentity = 0;

            foreach (var item in temp)
            {
                buyprice = buyprice + item.actualPrice;
                sellprice = sellprice + (item.sellPrice * item.quentity);
                quentity = quentity + item.quentity;
            }

            priceDetails.avgBuyPrice = (buyprice == 0) ? 0 : (buyprice / quentity);
            priceDetails.avgSellPrice = (sellprice == 0) ? 0 : (sellprice / quentity);
            await _operation.saveOrUpdateOrDeleteData(priceDetails);
        }
    }
}
