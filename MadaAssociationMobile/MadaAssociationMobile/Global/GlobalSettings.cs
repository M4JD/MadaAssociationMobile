using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using MadaAssociationMobile.Controls;
using MadaAssociationMobile.Global;
using MadaAssociationMobile.Interfaces;
using MadaAssociationMobile.Services.APIServices;
using MadaAssociationMobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MadaAssociationMobile.Common.Global
{
    public class GlobalSettings
    {
        public static readonly EnvironmentEnum Env = EnvironmentEnum.Debug;
        public static LoginResponse LoggedUser = null;
        //public static readonly string APIURL = "https://192.168.0.106:7200/";
        //public static readonly string APIURL = "http://m4jdim-001-site1.atempurl.com/";
        public static readonly string APIURL = "http://192.168.0.106:5145/";
        public static readonly string IOSAppCenterSecret = "747312d4-e6c4-4eb2-9899-03d1be099896";
        public static readonly string AndroidAppCenterSecret = "747312d4-e6c4-4eb2-9899-03d1be099896";
        public static readonly string DefaultDateTimeFormat = "dd MMMM yyyy";
        public static readonly string DatePickerDateTimeFormat = "dd-MM-yyyy";
        public static readonly string APIDatePickerDateFormat = "yyyy-MM-dd";
        public static readonly string APIDatePickerDateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        public static readonly bool HasProxyURL = false;
        public static readonly int DefaultRounding = 2;

        public static readonly int ImageUploadCallsTimeOut = 120;

        public static readonly string APICallsDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffff";
        //SINGLE INSTANTIATION
        public static readonly string PublicRSAKey = @"-----BEGIN PUBLIC KEY-----
                                                MIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEAt5bTK3UcjuTSZGsgvoYx
                                                VbM5zaPwaBnORg9/7+mLi+lbhdeHYHDgPBVk6dHv1o8yLUAL9rvcN7KopsSk6KdQ
                                                rpndrYYSiLkE5SMdvahm6QrYnKpAqAawNcA1tjUB3c3WxXoTbvpEQebEpGWo10Hi
                                                Vo8E7zprvHwZhVYKs3KRbC3qwrwbWj8u1jMAr8IXuB1HmIYzQOKnKK0rIbYCFTTT
                                                D1flcohzyYwL1KA+gOCxcWjx1EU12zU2WG+T9NAqZj+c17c5X9jhm2wMqUzsgBA1
                                                zN1RQ4ch76lLLL4bal/DQJIVh/FoL+mWA5FvWrVA+tZdyxgJ6H0jDiNzElh3ll2s
                                                fpIGYjLc+n8FEIcqgAp5xArIwZP3+zPDWYf9IeG8FJziMxwt0yjNNCOVHY+Au1IP
                                                Jwm6iQfPWsZuRYfuOwTUL3WaDDC6PQ509xSpmb7nylSR1cbnMt8y8yofzvKILotJ
                                                vpE2E+eyjkbEI9CoQFqufgZxJmslI5XRa+Nb5T34K1JrAgMBAAE=
                                                -----END PUBLIC KEY-----";
        private static GlobalSettings _Instance;
        public static GlobalSettings Instance
        {
            get
            {
                if (_Instance == null) _Instance = new GlobalSettings();
                return _Instance;
            }
        }
        public static ObservableCollection<TabItem> TabItems = null;
        public static string UDID
        {
            get
            {
                if (string.IsNullOrEmpty(AppSharedPrefences.Instance.UDID))
                {
                    AppSharedPrefences.Instance.UDID = DependencyService.Get<IDeviceInfo>().GetDeviceID();
                }
                return AppSharedPrefences.Instance.UDID;
            }
        }
        public static string FCM
        {
            get
            {
                AppSharedPrefences.Instance.FCMToken = DependencyService.Get<IFirebaseMessagingHelper>().GetFCMToken();
                return AppSharedPrefences.Instance.FCMToken;
            }
        }
        public static bool FCMChanged
        {
            get
            {
                return AppSharedPrefences.Instance.FCMTokenChanged;
            }
        }
        public static string OS
        {
            get
            {
                return Device.RuntimePlatform;
            }
        }
        public static List<Country> CountryList = null;

        public static List<Selectable> GenderList = null;
        public static Task GetGenderList()
        {
            if (GenderList != null) return Task.CompletedTask;
            List<Selectable> Genders = new List<Selectable>();
            Genders.Add(new Selectable
            {
                DisplayValue = "Male",
                Value = "M"
            });
            Genders.Add(new Selectable
            {
                DisplayValue = "Female",
                Value = "F"
            });
            GenderList = Genders;
            return Task.CompletedTask;
        }
        public static Task GetCountryList(List<Country> countries = null)
        {
            if (CountryList != null) return Task.CompletedTask;
            if (countries != null)
            {
                CountryList = countries;
                return Task.CompletedTask;
            }
            countries = new List<Country>();
            //countries.Add(new Country
            //{
            //    Code = "+7 840",
            //    CountryCode = null,
            //    Name = "Abkhazia"
            //});

            countries.Add(new Country
            {
                Code = "+1 264",
                CountryCode = "AI",
                Name = "Anguilla"
            });
            countries.Add(new Country
            {
                Code = "+213",
                CountryCode = "AL",
                Name = "Albania",
                Nationality = "Albanian"
            });
            countries.Add(new Country
            {
                Code = "+355",
                CountryCode = "DZ",
                Name = "Algeria",
                Nationality = "Algerian"
            });
            countries.Add(new Country
            {
                Code = "+1 684",
                CountryCode = "AS",
                Name = "American Samoa"
            });
            countries.Add(new Country
            {
                Code = "+376",
                CountryCode = "AD",
                Name = "Andorra"
            });
            countries.Add(new Country
            {
                Code = "+244",
                CountryCode = "AO",
                Name = "Angola"
            });
            countries.Add(new Country
            {
                Code = "+1 268",
                CountryCode = "AG",
                Name = "Antigua and Barbuda"
            });
            countries.Add(new Country
            {
                Code = "+54",
                CountryCode = "AR",
                Name = "Argentina",
                Nationality = "Argentinian"
            });
            countries.Add(new Country
            {
                Code = "+374",
                CountryCode = "AM",
                Name = "Armenia"
            });
            countries.Add(new Country
            {
                Code = "+297",
                CountryCode = "AW",
                Name = "Aruba"
            });

            //countries.Add(new Country
            //{
            //    Code = "+247",
            //    CountryCode = null,
            //    Name = "Ascension"
            //});
            countries.Add(new Country
            {
                Code = "+61",
                CountryCode = "AU",
                Name = "Australia",
                Nationality = "Australian"
            });
            //countries.Add(new Country
            //{
            //    Code = "+672",
            //    CountryCode = null,
            //    Name = "Australian External Territories"
            //});
            countries.Add(new Country
            {
                Code = "+43",
                CountryCode = "AT",
                Name = "Austria",
                Nationality = "Austrian"
            });

            countries.Add(new Country
            {
                Code = "+994",
                CountryCode = "AZ",
                Name = "Azerbaijan"
            });
            countries.Add(new Country
            {
                Code = "+1 242",
                CountryCode = "BS",
                Name = "Bahamas"
            });
            countries.Add(new Country
            {
                Code = "+973",
                CountryCode = "BH",
                Name = "Bahrain"
            });
            countries.Add(new Country
            {
                Code = "+880",
                CountryCode = "BD",
                Name = "Bangladesh",
                Nationality = "Bangladeshi"
            });

            countries.Add(new Country
            {
                Code = "+1 246",
                CountryCode = "BB",
                Name = "Barbados"
            });
            //countries.Add(new Country
            //{
            //    Code = "+1 268",
            //    CountryCode = "AG",
            //    Name = "Barbuda"
            //});
            countries.Add(new Country
            {
                Code = "+375",
                CountryCode = "BY",
                Name = "Belarus"
            });
            countries.Add(new Country
            {
                Code = "+32",
                CountryCode = "BE",
                Name = "Belgium",
                Nationality = "Belgian"
            });

            countries.Add(new Country
            {
                Code = "+501",
                CountryCode = "BZ",
                Name = "Belize"
            });
            countries.Add(new Country
            {
                Code = "+229",
                CountryCode = "BJ",
                Name = "Benin"
            });
            countries.Add(new Country
            {
                Code = "+1 441",
                CountryCode = "BM",
                Name = "Bermuda"
            });
            countries.Add(new Country
            {
                Code = "+975",
                CountryCode = "BT",
                Name = "Bhutan"
            });

            countries.Add(new Country
            {
                Code = "+591",
                CountryCode = "BO",
                Name = "Bolivia",
                Nationality = "Bolivian"
            });
            countries.Add(new Country
            {
                Code = "+387",
                CountryCode = "BA",
                Name = "Bosnia and Herzegovina"
            });
            countries.Add(new Country
            {
                Code = "+267",
                CountryCode = "BW",
                Name = "Botswana",
                Nationality = "Batswana"
            });
            countries.Add(new Country
            {
                Code = "+55",
                CountryCode = "BR",
                Name = "Brazil",
                Nationality = "Brazilian"
            });

            countries.Add(new Country
            {
                Code = "+246",
                CountryCode = "IO",
                Name = "British Indian Ocean Territory"
            });
            countries.Add(new Country
            {
                Code = "+1 284",
                CountryCode = "VG",
                Name = "British Virgin Islands"
            });
            countries.Add(new Country
            {
                Code = "+673",
                CountryCode = "BN",
                Name = "Brunei"
            });
            countries.Add(new Country
            {
                Code = "+359",
                CountryCode = "BG",
                Name = "Bulgaria",
                Nationality = "Bulgarian"
            });

            countries.Add(new Country
            {
                Code = "+226",
                CountryCode = "BF",
                Name = "Burkina Faso"
            });
            countries.Add(new Country
            {
                Code = "+257",
                CountryCode = "BI",
                Name = "Burundi"
            });
            countries.Add(new Country
            {
                Code = "+855",
                CountryCode = "KH",
                Name = "Cambodia",
                Nationality = "Cambodian"
            });
            countries.Add(new Country
            {
                Code = "+237",
                CountryCode = "CM",
                Name = "Cameroon",
                Nationality = "Cameroonian"
            });

            countries.Add(new Country
            {
                Code = "+1",
                CountryCode = "CA",
                Name = "Canada",
                Nationality = "Canadian"
            });
            countries.Add(new Country
            {
                Code = "+238",
                CountryCode = "CV",
                Name = "Cape Verde"
            });
            countries.Add(new Country
            {
                Code = "+345",
                CountryCode = "KY",
                Name = "Cayman Islands"
            });
            countries.Add(new Country
            {
                Code = "+236",
                CountryCode = "CF",
                Name = "Central African Republic"
            });

            countries.Add(new Country
            {
                Code = "+235",
                CountryCode = "TD",
                Name = "Chad"
            });
            countries.Add(new Country
            {
                Code = "+56",
                CountryCode = "CL",
                Name = "Chile",
                Nationality = "Chilean"
            });
            countries.Add(new Country
            {
                Code = "+86",
                CountryCode = "CN",
                Name = "China",
                Nationality = "Chinese"
            });
            countries.Add(new Country
            {
                Code = "+61",
                CountryCode = "CX",
                Name = "Christmas Island"
            });

            //countries.Add(new Country
            //{
            //    Code = "+61",
            //    CountryCode = null,
            //    Name = "Cocos-Keeling Islands"
            //});
            countries.Add(new Country
            {
                Code = "+57",
                CountryCode = "CO",
                Name = "Colombia",
                Nationality = "Colombian"
            });
            countries.Add(new Country
            {
                Code = "+269",
                CountryCode = "KM",
                Name = "Comoros"
            });
            countries.Add(new Country
            {
                Code = "+242",
                CountryCode = "CG",
                Name = "Congo"
            });

            countries.Add(new Country
            {
                Code = "+243",
                CountryCode = "CD",
                Name = "Congo, Dem. Rep. of (Zaire)"
            });
            countries.Add(new Country
            {
                Code = "+682",
                CountryCode = "CK",
                Name = "Cook Islands"
            });
            countries.Add(new Country
            {
                Code = "+506",
                CountryCode = "CR",
                Name = "Costa Rica",
                Nationality = "Costa Rican"
            });
            countries.Add(new Country
            {
                Code = "+385",
                CountryCode = "HR",
                Name = "Croatia",
                Nationality = "Croatian"
            });

            countries.Add(new Country
            {
                Code = "+53",
                CountryCode = "CU",
                Name = "Cuba",
                Nationality = "Cuban"
            });
            countries.Add(new Country
            {
                Code = "+599",
                CountryCode = "CW",
                Name = "Curacao"
            });
            countries.Add(new Country
            {
                Code = "+537",
                CountryCode = "CY",
                Name = "Cyprus"
            });
            countries.Add(new Country
            {
                Code = "+420",
                CountryCode = "CZ",
                Name = "Czech Republic",
                Nationality = "Czech"
            });

            countries.Add(new Country
            {
                Code = "+45",
                CountryCode = "DK",
                Name = "Denmark",
                Nationality = "Danish"
            });
            //countries.Add(new Country
            //{
            //    Code = "+246",
            //    CountryCode = null,
            //    Name = "Diego Garcia"
            //});
            countries.Add(new Country
            {
                Code = "+253",
                CountryCode = "DJ",
                Name = "Djibouti"
            });
            countries.Add(new Country
            {
                Code = "+1 767",
                CountryCode = "DM",
                Name = "Dominica"
            });

            countries.Add(new Country
            {
                Code = "+1 809",
                CountryCode = "DO",
                Name = "Dominican Republic",
                Nationality = "Dominican"
            });
            countries.Add(new Country
            {
                Code = "+670",
                CountryCode = "TL",
                Name = "East Timor"
            });
            //countries.Add(new Country
            //{
            //    Code = "+56",
            //    CountryCode = null,
            //    Name = "Easter Island"
            //});
            countries.Add(new Country
            {
                Code = "+593",
                CountryCode = "EC",
                Name = "Ecuador",
                Nationality = "Ecuadorian"
            });

            countries.Add(new Country
            {
                Code = "+20",
                CountryCode = "EG",
                Name = "Egypt",
                Nationality = "Egyptian"
            });
            countries.Add(new Country
            {
                Code = "+503",
                CountryCode = "SV",
                Name = "El Salvador",
                Nationality = "Salvadorian"
            });
            countries.Add(new Country
            {
                Code = "+44",
                CountryCode = "GB-ENG",
                Name = "England",
                Nationality = "English"
            });
            countries.Add(new Country
            {
                Code = "+240",
                CountryCode = "GQ",
                Name = "Equatorial Guinea"
            });
            countries.Add(new Country
            {
                Code = "+291",
                CountryCode = "ER",
                Name = "Eritrea"
            });

            countries.Add(new Country
            {
                Code = "+372",
                CountryCode = "EE",
                Name = "Estonia",
                Nationality = "Estonian"
            });
            countries.Add(new Country
            {
                Code = "+251",
                CountryCode = "ET",
                Name = "Ethiopia",
                Nationality = "Ethiopian"
            });
            countries.Add(new Country
            {
                Code = "+500",
                CountryCode = "FK",
                Name = "Falkland Islands"
            });
            countries.Add(new Country
            {
                Code = "+298",
                CountryCode = "FO",
                Name = "Faroe Islands"
            });

            countries.Add(new Country
            {
                Code = "+679",
                CountryCode = "FJ",
                Name = "Fiji",
                Nationality = "Fijian"
            });
            countries.Add(new Country
            {
                Code = "+358",
                CountryCode = "FI",
                Name = "Finland",
                Nationality = "Finnish"
            });
            countries.Add(new Country
            {
                Code = "+33",
                CountryCode = "FR",
                Name = "France",
                Nationality = "French"
            });
            //countries.Add(new Country
            //{
            //    Code = "+596",
            //    CountryCode = null,
            //    Name = "French Antilles"
            //});

            countries.Add(new Country
            {
                Code = "+594",
                CountryCode = "GF",
                Name = "French Guiana"
            });
            countries.Add(new Country
            {
                Code = "+689",
                CountryCode = "PF",
                Name = "French Polynesia"
            });
            countries.Add(new Country
            {
                Code = "+241",
                CountryCode = "GA",
                Name = "Gabon"
            });
            countries.Add(new Country
            {
                Code = "+220",
                CountryCode = "GM",
                Name = "Gambia"
            });

            countries.Add(new Country
            {
                Code = "+995",
                CountryCode = "GE",
                Name = "Georgia"
            });
            countries.Add(new Country
            {
                Code = "+49",
                CountryCode = "DE",
                Name = "Germany",
                Nationality = "German"
            });
            countries.Add(new Country
            {
                Code = "+233",
                CountryCode = "GH",
                Name = "Ghana",
                Nationality = "Ghanaian"
            });
            countries.Add(new Country
            {
                Code = "+350",
                CountryCode = "GI",
                Name = "Gibraltar"
            });
            countries.Add(new Country
            {
                Code = "+30",
                CountryCode = "GR",
                Name = "Greece",
                Nationality = "Greek"
            });
            countries.Add(new Country
            {
                Code = "+299",
                CountryCode = "GL",
                Name = "Greenland"
            });
            countries.Add(new Country
            {
                Code = "+1 473",
                CountryCode = "GD",
                Name = "Grenada"
            });
            countries.Add(new Country
            {
                Code = "+590",
                CountryCode = "GP",
                Name = "Guadeloupe"
            });

            countries.Add(new Country
            {
                Code = "+1 671",
                CountryCode = "GU",
                Name = "Guam"
            });
            countries.Add(new Country
            {
                Code = "+502",
                CountryCode = "GT",
                Name = "Guatemala",
                Nationality = "Guatemalan"
            });
            countries.Add(new Country
            {
                Code = "+224",
                CountryCode = "GN",
                Name = "Guinea"
            });
            countries.Add(new Country
            {
                Code = "+245",
                CountryCode = "GW",
                Name = "Guinea-Bissau"
            });


            countries.Add(new Country
            {
                Code = "+595",
                CountryCode = "GY",
                Name = "Guyana"
            });
            countries.Add(new Country
            {
                Code = "+509",
                CountryCode = "HT",
                Name = "Haiti",
                Nationality = "Haitian"
            });
            countries.Add(new Country
            {
                Code = "+504",
                CountryCode = "HN",
                Name = "Honduras",
                Nationality = "Honduran"
            });
            countries.Add(new Country
            {
                Code = "+852",
                CountryCode = "HK",
                Name = "Hong Kong SAR China"
            });
            countries.Add(new Country
            {
                Code = "+36",
                CountryCode = "HU",
                Name = "Hungary",
                Nationality = "Hungarian"
            });
            countries.Add(new Country
            {
                Code = "+354",
                CountryCode = "IS",
                Name = "Iceland",
                Nationality = "Icelandic"
            });
            countries.Add(new Country
            {
                Code = "+91",
                CountryCode = "IN",
                Name = "India",
                Nationality = "Indian"
            });
            countries.Add(new Country
            {
                Code = "+62",
                CountryCode = "ID",
                Name = "Indonesia",
                Nationality = "Indonesian"
            });

            countries.Add(new Country
            {
                Code = "+98",
                CountryCode = "IR",
                Name = "Iran",
                Nationality = "Iranian"
            });
            countries.Add(new Country
            {
                Code = "+964",
                CountryCode = "IQ",
                Name = "Iraq",
                Nationality = "Iraqi"
            });
            countries.Add(new Country
            {
                Code = "+353",
                CountryCode = "IE",
                Name = "Ireland",
                Nationality = "Irish"
            });
            //countries.Add(new Country
            //{
            //    Code = "+972",
            //    CountryCode = "IL",
            //    Name = "Israel",
            //    Nationality = "Israeli"
            //});

            countries.Add(new Country
            {
                Code = "+39",
                CountryCode = "IT",
                Name = "Italy",
                Nationality = "Italian"
            });
            countries.Add(new Country
            {
                Code = "+225",
                CountryCode = "CI",
                Name = "Ivory Coast"
            });
            countries.Add(new Country
            {
                Code = "+1 876",
                CountryCode = "JM",
                Name = "Jamaica",
                Nationality = "Jamaican"
            });
            countries.Add(new Country
            {
                Code = "+81",
                CountryCode = "JP",
                Name = "Japan",
                Nationality = "Japanese"
            });

            countries.Add(new Country
            {
                Code = "+962",
                CountryCode = "JO",
                Name = "Jordan",
                Nationality = "Jordanian"
            });
            countries.Add(new Country
            {
                Code = "+7 7",
                CountryCode = "KZ",
                Name = "Kazakhstan"
            });
            countries.Add(new Country
            {
                Code = "+254",
                CountryCode = "KE",
                Name = "Kenya",
                Nationality = "Kenyan"
            });
            countries.Add(new Country
            {
                Code = "+686",
                CountryCode = "KI",
                Name = "Kiribati"
            });

            countries.Add(new Country
            {
                Code = "+965",
                CountryCode = "KW",
                Name = "Kuwait",
                Nationality = "Kuwaiti"
            });
            countries.Add(new Country
            {
                Code = "+996",
                CountryCode = "KG",
                Name = "Kyrgyzstan"
            });
            countries.Add(new Country
            {
                Code = "+856",
                CountryCode = "LA",
                Name = "Laos",
                Nationality = "Lao"
            });
            countries.Add(new Country
            {
                Code = "+371",
                CountryCode = "LV",
                Name = "Latvia",
                Nationality = "Latvian"
            });

            countries.Add(new Country
            {
                Code = "+961",
                CountryCode = "LB",
                Name = "Lebanon",
                Nationality = "Lebanese"
            });
            countries.Add(new Country
            {
                Code = "+266",
                CountryCode = "LS",
                Name = "Lesotho"
            });
            countries.Add(new Country
            {
                Code = "+231",
                CountryCode = "LR",
                Name = "Liberia"
            });
            countries.Add(new Country
            {
                Code = "+218",
                CountryCode = "LY",
                Name = "Libya",
                Nationality = "Libyan"
            });

            countries.Add(new Country
            {
                Code = "+423",
                CountryCode = "LI",
                Name = "Liechtenstein"
            });
            countries.Add(new Country
            {
                Code = "+370",
                CountryCode = "LT",
                Name = "Lithuania",
                Nationality = "Lithuanian"
            });
            countries.Add(new Country
            {
                Code = "+352",
                CountryCode = "LU",
                Name = "Luxembourg"
            });
            countries.Add(new Country
            {
                Code = "+853",
                CountryCode = "MO",
                Name = "Macau SAR China"
            });

            countries.Add(new Country
            {
                Code = "+389",
                CountryCode = "MK",
                Name = "Macedonia"
            });
            countries.Add(new Country
            {
                Code = "+261",
                CountryCode = "MG",
                Name = "Madagascar",
                Nationality = "Malagasy"
            });
            countries.Add(new Country
            {
                Code = "+265",
                CountryCode = "MW",
                Name = "Malawi"
            });
            countries.Add(new Country
            {
                Code = "+60",
                CountryCode = "MY",
                Name = "Malaysia",
                Nationality = "Malaysian"
            });

            countries.Add(new Country
            {
                Code = "+960",
                CountryCode = "MV",
                Name = "Maldives"
            });
            countries.Add(new Country
            {
                Code = "+223",
                CountryCode = "ML",
                Name = "Mali",
                Nationality = "Malian"
            });
            countries.Add(new Country
            {
                Code = "+356",
                CountryCode = "MT",
                Name = "Malta",
                Nationality = "Maltese"
            });
            countries.Add(new Country
            {
                Code = "+692",
                CountryCode = "MH",
                Name = "Marshall Islands"
            });

            countries.Add(new Country
            {
                Code = "+596",
                CountryCode = "MQ",
                Name = "Martinique"
            });
            countries.Add(new Country
            {
                Code = "+222",
                CountryCode = "MR",
                Name = "Mauritania"
            });
            countries.Add(new Country
            {
                Code = "+230",
                CountryCode = "MU",
                Name = "Mauritius"
            });
            countries.Add(new Country
            {
                Code = "+262",
                CountryCode = "YT",
                Name = "Mayotte"
            });

            countries.Add(new Country
            {
                Code = "+52",
                CountryCode = "MX",
                Name = "Mexico",
                Nationality = "Mexican"
            });
            countries.Add(new Country
            {
                Code = "+691",
                CountryCode = "FM",
                Name = "Micronesia"
            });
            //countries.Add(new Country
            //{
            //    Code = "+1 808",
            //    CountryCode = null,
            //    Name = "Midway Island"
            //});
            countries.Add(new Country
            {
                Code = "+373",
                CountryCode = "MD",
                Name = "Moldova"
            });

            countries.Add(new Country
            {
                Code = "+377",
                CountryCode = "MC",
                Name = "Monaco"
            });
            countries.Add(new Country
            {
                Code = "+976",
                CountryCode = "MN",
                Name = "Mongolia",
                Nationality = "Mongolian"
            });
            countries.Add(new Country
            {
                Code = "+382",
                CountryCode = "ME",
                Name = "Montenegro"
            });
            countries.Add(new Country
            {
                Code = "+1664",
                CountryCode = "MS",
                Name = "Montserrat"
            });
            countries.Add(new Country
            {
                Code = "+258",
                CountryCode = "MZ",
                Name = "Mozambique",
                Nationality = "Mozambican"
            });

            countries.Add(new Country
            {
                Code = "+212",
                CountryCode = "MA",
                Name = "Morocco",
                Nationality = "Moroccan"
            });
            countries.Add(new Country
            {
                Code = "+95",
                CountryCode = "MM",
                Name = "Myanmar"
            });
            countries.Add(new Country
            {
                Code = "+264",
                CountryCode = "NA",
                Name = "Namibia",
                Nationality = "Namibian"
            });
            countries.Add(new Country
            {
                Code = "+674",
                CountryCode = "NR",
                Name = "Nauru"
            });

            countries.Add(new Country
            {
                Code = "+977",
                CountryCode = "NP",
                Name = "Nepal",
                Nationality = "Nepalese"
            });
            countries.Add(new Country
            {
                Code = "+31",
                CountryCode = "NL",
                Name = "Netherlands",
                Nationality = "Dutch"
            });
            countries.Add(new Country
            {
                Code = "+599",
                CountryCode = "AN",
                Name = "Netherlands Antilles"
            });
            countries.Add(new Country
            {
                Code = "+1 869",
                CountryCode = "KN",
                Name = "Nevis"
            });

            countries.Add(new Country
            {
                Code = "+687",
                CountryCode = "NC",
                Name = "New Caledonia"
            });
            countries.Add(new Country
            {
                Code = "+64",
                CountryCode = "NZ",
                Name = "New Zealand",
                Nationality = "New Zealand"
            });
            countries.Add(new Country
            {
                Code = "+505",
                CountryCode = "NI",
                Name = "Nicaragua",
                Nationality = "Nicaraguan"
            });
            countries.Add(new Country
            {
                Code = "+227",
                CountryCode = "NE",
                Name = "Niger"
            });

            countries.Add(new Country
            {
                Code = "+234",
                CountryCode = "NG",
                Name = "Nigeria",
                Nationality = "Nigerian"
            });
            countries.Add(new Country
            {
                Code = "+683",
                CountryCode = "NU",
                Name = "Niue"
            });
            countries.Add(new Country
            {
                Code = "+672",
                CountryCode = "NF",
                Name = "Norfolk Island"
            });
            countries.Add(new Country
            {
                Code = "+850",
                CountryCode = "KP",
                Name = "North Korea"
            });

            countries.Add(new Country
            {
                Code = "+675",
                CountryCode = "PG",
                Name = "Papua New Guinea"
            });
            countries.Add(new Country
            {
                Code = "+507",
                CountryCode = "PA",
                Name = "Panama",
                Nationality = "Panamanian"
            });
            countries.Add(new Country
            {
                Code = "+970",
                CountryCode = "PS",
                Name = "Palestine",
                Nationality = "Palestinian"
            });
            countries.Add(new Country
            {
                Code = "+680",
                CountryCode = "PW",
                Name = "Palau"
            });

            countries.Add(new Country
            {
                Code = "+92",
                CountryCode = "PK",
                Name = "Pakistan",
                Nationality = "Pakistani"
            });
            countries.Add(new Country
            {
                Code = "+968",
                CountryCode = "OM",
                Name = "Oman"
            });
            countries.Add(new Country
            {
                Code = "+47",
                CountryCode = "NO",
                Name = "Norway",
                Nationality = "Norwegian"
            });
            countries.Add(new Country
            {
                Code = "+1 670",
                CountryCode = "MP",
                Name = "Northern Mariana Islands"
            });

            countries.Add(new Country
            {
                Code = "+595",
                CountryCode = "PY",
                Name = "Paraguay",
                Nationality = "Paraguayan"
            });
            countries.Add(new Country
            {
                Code = "+51",
                CountryCode = "PY",
                Name = "Peru",
                Nationality = "Peruvian"
            });
            countries.Add(new Country
            {
                Code = "+63",
                CountryCode = "PH",
                Name = "Philippines",
                Nationality = "Philippine"
            });
            countries.Add(new Country
            {
                Code = "+48",
                CountryCode = "PL",
                Name = "Poland",
                Nationality = "Polish"
            });

            countries.Add(new Country
            {
                Code = "+351",
                CountryCode = "PT",
                Name = "Portugal",
                Nationality = "Portuguese"
            });
            countries.Add(new Country
            {
                Code = "+1 787",
                CountryCode = "PR",
                Name = "Puerto Rico"
            });
            countries.Add(new Country
            {
                Code = "+974",
                CountryCode = "QA",
                DefaultPhoneNumber = "44026888",
                Name = "Qatar",
                Nationality = "Qatari"
            });
            countries.Add(new Country
            {
                Code = "+262",
                CountryCode = "RE",
                Name = "Reunion"
            });

            countries.Add(new Country
            {
                Code = "+40",
                CountryCode = "RO",
                Name = "Romania",
                Nationality = "Romanian"
            });
            countries.Add(new Country
            {
                Code = "+7",
                CountryCode = "RU",
                Name = "Russia",
                Nationality = "Russian"
            });
            countries.Add(new Country
            {
                Code = "+250",
                CountryCode = "RW",
                Name = "Rwanda"
            });
            countries.Add(new Country
            {
                Code = "+685",
                CountryCode = "WS",
                Name = "Samoa"
            });

            countries.Add(new Country
            {
                Code = "+378",
                CountryCode = "SM",
                Name = "San Marino"
            });
            countries.Add(new Country
            {
                Code = "+966",
                CountryCode = "SA",
                Name = "Saudi Arabia",
                Nationality = "Saudi"
            });
            countries.Add(new Country
            {
                Code = "+44",
                CountryCode = "GB-SCT",
                Name = "Scotland",
                Nationality = "Scottish"
            });
            countries.Add(new Country
            {
                Code = "+221",
                CountryCode = "SN",
                Name = "Senegal",
                Nationality = "Senegalese"
            });
            countries.Add(new Country
            {
                Code = "+381",
                CountryCode = "RS",
                Name = "Serbia",
                Nationality = "Serbian"
            });

            countries.Add(new Country
            {
                Code = "+248",
                CountryCode = "SC",
                Name = "Seychelles"
            });
            countries.Add(new Country
            {
                Code = "+232",
                CountryCode = "SL",
                Name = "Sierra Leone"
            });
            countries.Add(new Country
            {
                Code = "+65",
                CountryCode = "SG",
                Name = "Singapore",
                Nationality = "Singaporean"
            });
            countries.Add(new Country
            {
                Code = "+421",
                CountryCode = "SK",
                Name = "Slovakia",
                Nationality = "Slovak"
            });

            countries.Add(new Country
            {
                Code = "+386",
                CountryCode = "SI",
                Name = "Slovenia"
            });
            countries.Add(new Country
            {
                Code = "+677",
                CountryCode = "SB",
                Name = "Solomon Islands"
            });
            countries.Add(new Country
            {
                Code = "+27",
                CountryCode = "ZA",
                Name = "South Africa",
                Nationality = "South African"
            });
            countries.Add(new Country
            {
                Code = "+500",
                CountryCode = "GS",
                Name = "South Georgia and the South Sandwich Islands"
            });

            countries.Add(new Country
            {
                Code = "+82",
                CountryCode = "KR",
                Name = "South Korea",
                Nationality = "Korean"
            });
            countries.Add(new Country
            {
                Code = "+34",
                CountryCode = "ES",
                Name = "Spain",
                Nationality = "Spanish"
            });
            countries.Add(new Country
            {
                Code = "+94",
                CountryCode = "LK",
                Name = "Sri Lanka",
                Nationality = "Sri Lankan"
            });
            countries.Add(new Country
            {
                Code = "+249",
                CountryCode = "SD",
                Name = "Sudan",
                Nationality = "Sudanese"
            });

            countries.Add(new Country
            {
                Code = "+597",
                CountryCode = "SR",
                Name = "Suriname"
            });
            countries.Add(new Country
            {
                Code = "+268",
                CountryCode = "SZ",
                Name = "Swaziland"
            });
            countries.Add(new Country
            {
                Code = "+46",
                CountryCode = "SE",
                Name = "Sweden",
                Nationality = "Swedish"
            });
            countries.Add(new Country
            {
                Code = "+41",
                CountryCode = "CH",
                Name = "Switzerland",
                Nationality = "Swiss"
            });

            countries.Add(new Country
            {
                Code = "+963",
                CountryCode = "SY",
                Name = "Syria",
                Nationality = "Syrian"
            });
            countries.Add(new Country
            {
                Code = "+886",
                CountryCode = "TW",
                Name = "Taiwan",
                Nationality = "Taiwanese"
            });
            countries.Add(new Country
            {
                Code = "+992",
                CountryCode = "TJ",
                Name = "Tajikistan",
                Nationality = "Tajikistani"
            });
            countries.Add(new Country
            {
                Code = "+255",
                CountryCode = "TZ",
                Name = "Tanzania",
                Nationality = "Tanzanian"
            });

            countries.Add(new Country
            {
                Code = "+66",
                CountryCode = "TH",
                Name = "Thailand",
                Nationality = "Thai"
            });
            //countries.Add(new Country
            //{
            //    Code = "+670",
            //    CountryCode = null,
            //    Name = "Timor Leste"
            //});
            countries.Add(new Country
            {
                Code = "+228",
                CountryCode = "TG",
                Name = "Togo"
            });
            countries.Add(new Country
            {
                Code = "+690",
                CountryCode = "TK",
                Name = "Tokelau"
            });

            countries.Add(new Country
            {
                Code = "+676",
                CountryCode = "TO",
                Name = "Tonga",
                Nationality = "Tongan"
            });
            countries.Add(new Country
            {
                Code = "+1 868",
                CountryCode = "TT",
                Name = "Trinidad and Tobago"
            });
            countries.Add(new Country
            {
                Code = "+216",
                CountryCode = "TN",
                Name = "Tunisia",
                Nationality = "Tunisian"
            });
            countries.Add(new Country
            {
                Code = "+90",
                CountryCode = "TR",
                Name = "Turkey",
                Nationality = "Turkish"
            });

            countries.Add(new Country
            {
                Code = "+993",
                CountryCode = "TM",
                Name = "Turkmenistan"
            });
            countries.Add(new Country
            {
                Code = "+1 649",
                CountryCode = "TC",
                Name = "Turks and Caicos Islands"
            });
            countries.Add(new Country
            {
                Code = "+688",
                CountryCode = "TV",
                Name = "Tuvalu"
            });
            countries.Add(new Country
            {
                Code = "+1 340",
                CountryCode = "VI",
                Name = "U.S. Virgin Islands"
            });

            countries.Add(new Country
            {
                Code = "+256",
                CountryCode = "UG",
                Name = "Uganda"
            });
            countries.Add(new Country
            {
                Code = "+380",
                CountryCode = "UA",
                Name = "Netherlands"
            });
            countries.Add(new Country
            {
                Code = "+971",
                CountryCode = "AE",
                Name = "United Arab Emirates",
                Nationality = "Emirati"
            });
            countries.Add(new Country
            {
                Code = "+44",
                CountryCode = "GB",
                Name = "United Kingdom",
                Nationality = "British"
            });

            countries.Add(new Country
            {
                Code = "+1",
                CountryCode = "US",
                Name = "United States",
                Nationality = "American"
            });
            countries.Add(new Country
            {
                Code = "+598",
                CountryCode = "UY",
                Name = "Uruguay",
                Nationality = "Uruguayan"
            });
            countries.Add(new Country
            {
                Code = "+998",
                CountryCode = "UZ",
                Name = "Uzbekistan"
            });
            countries.Add(new Country
            {
                Code = "+678",
                CountryCode = "VU",
                Name = "Vanuatu"
            });

            countries.Add(new Country
            {
                Code = "+58",
                CountryCode = "VE",
                Name = "Venezuela",
                Nationality = "Venezuelan"
            });
            countries.Add(new Country
            {
                Code = "+84",
                CountryCode = "VN",
                Name = "Vietnam",
                Nationality = "Vietnamese"
            });
            countries.Add(new Country
            {
                Code = "+44",
                CountryCode = "GB-WLS",
                Name = "Wales",
                Nationality = "Welsh"
            });
            //countries.Add(new Country
            //{
            //    Code = "+1 808",
            //    CountryCode = null,
            //    Name = "Wake Island"
            //});
            countries.Add(new Country
            {
                Code = "+681",
                CountryCode = "WF",
                Name = "Wallis and Futuna"
            });

            countries.Add(new Country
            {
                Code = "+967",
                CountryCode = "YE",
                Name = "Yemen"
            });
            countries.Add(new Country
            {
                Code = "+260",
                CountryCode = "ZM",
                Name = "Zambia",
                Nationality = "Zambian"
            });
            //countries.Add(new Country
            //{
            //    Code = "+255",
            //    CountryCode = null,
            //    Name = "Zanzibar"
            //});
            countries.Add(new Country
            {
                Code = "+263",
                CountryCode = "ZW",
                Name = "Zimbabwe",
                Nationality = "Zimbabwean"
            });
            CountryList = countries;
            return Task.CompletedTask;
        }
    }
}
