using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static APPROG7311.Pages.DisasterModel;
using static APPROG7311.Pages.GoodsModel;
using static APPROG7311.Pages.MonetrayModel;

namespace APPROG7311.Pages.Admin
{
    public class AllocationModel : PageModel
    {
        public AllocationInfo paymentInfo = new AllocationInfo();
        public MonetrayInfo monetrayInfo = new MonetrayInfo();
        public List<MonetrayInfo> listmon = new List<MonetrayInfo>();
        public GoodsInfo goodsInfo = new GoodsInfo();
        public List<GoodsInfo> listgoo = new List<GoodsInfo>();
        public DisasterInfo disasterInfo = new DisasterInfo();
        public List<DisasterInfo> listdis = new List<DisasterInfo>();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public class AllocationInfo
        {
            public String ID;
            public String PAYER_NAME;
            public Double PAYMENT;
            public String ACTIVE_DISASTER;


        }
    }
}
