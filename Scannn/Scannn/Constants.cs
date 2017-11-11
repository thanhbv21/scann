using System;

namespace Scannn
{
    public static class Constants
    {
        public static string RestProductUrl = "https://api.ezcheck.vn/check3_service_app/app/getitembycode/{0}";

        public static string RestCompanyUrl = "https://api.ezcheck.vn/check3_service_app/app/getcompanybycode/{0}";

        public static string RestImageUrl = "https://api.ezcheck.vn/check3_service_app/app/getbatchimagebycode/{0}";

        public static string RestNewsUrl = "http://10.171.20.115:8080/check3_service_app/app/gethome/1";

        public static string RestUpdateLocationUrl = "https://api.ezcheck.vn/check3_service_app/app/updatelocation";

        public static string RestPurchaseUrl = "https://api.ezcheck.vn/check3_service_app/app/purchasebycode";

        public static string RestLoginUrl = "https://api.ezcheck.vn//check3_service_dashboard/user/signin";

        public static string RestLogoutUrl = "https://api.ezcheck.vn//check3_service_dashboard/user/signout";

        public static string RestGetProfileWithoutSSUrl = "https://api.ezcheck.vn//check3_service_dashboard/user/getprofilewithousession";

        public static string ColorPrimary = "#5e2abb";

        public static string ColorSecondary = "#d2c1f1";

        public static string WebUrl = "https://api.ezcheck.vn/check/{0}";

        public static string WebLHYDUrl = "https://ezcheck.vn/loihayydep/{0}";

        public static string RestLHYDUrl = "https://api.ezcheck.vn/check3_service_admin/loihay/getloihayydep/{0}";
    }
}
