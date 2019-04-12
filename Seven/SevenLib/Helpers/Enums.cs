using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib.Helpers
{
    public enum Genre
    {
        Fantasy,
        SF,
        Comedy,
        Romance,
        Mystery,
        Philosophy,
        Fable,
        Tragedy,
        Comics,
    }

    public enum Nationality
    {
        [Description("Afghanistan")]
        AF,

        [Description("Albania")]
        AL,

        [Description("Algeria")]
        DZ,

        [Description("American Samoa")]
        AS,

        [Description("Andorra")]
        AD,

        [Description("Angola")]
        AO,

        [Description("Anguilla")]
        AI,

        [Description("Antarctica")]
        AQ,

        [Description("Antigua and Barbuda")]
        AG,

        [Description("Argentina")]
        AR,

        [Description("Armenia")]
        AM,

        [Description("Aruba")]
        AW,

        [Description("Australia")]
        AU,

        [Description("Austria")]
        AT,

        [Description("Bahamas")]
        BS,

        [Description("Bangladesh")]
        BD,

        [Description("Belgium")]
        BE,

        [Description("Benin")]
        BJ,

        [Description("Bolivia")]
        BO,

        [Description("Bosnia and Herzegovina")]
        BA,

        [Description("Brazil")]
        BR,

        [Description("Bulgaria")]
        BG,

        [Description("Burkina Faso")]
        BF,

        [Description("Cambodia")]
        KH,

        [Description("Cameroon")]
        CM,

        [Description("Canada")]
        CA,

        [Description("Cayman Islands")]
        KY,

        [Description("Central African Republic")]
        CF,

        [Description("Chad")]
        TD,

        [Description("Chile")]
        CL,

        [Description("China")]
        CN,

        [Description("Christmas Island")]
        CX,

        [Description("Colombia")]
        CO,

        [Description("Congo")]
        CG,

        [Description("Democratic Republic of the Congo")]
        CD,

        [Description("Costa Rica")]
        CR,

        [Description("Croatia")]
        HR,

        [Description("Cuba")]
        CU,

        [Description("Czech Republic")]
        CZ,

        [Description("Cote d'Ivoire")]
        CI,

        [Description("Denmark")]
        DK,

        [Description("Djibouti")]
        DJ,

        [Description("Dominican Republic")]
        DO,

        [Description("Egypt")]
        EG,

        [Description("Estonia")]
        EE,

        [Description("Finland")]
        FI,

        [Description("France")]
        FR,

        [Description("Georgia")]
        GE,

        [Description("Germany")]
        DE,

        [Description("Greece")]
        GR,

        [Description("Greenland")]
        GL,

        [Description("Guyana")]
        GY,

        [Description("Heard Island and McDonald Islands")]
        HM,

        [Description("Hong Kong")]
        HK,

        [Description("Hungary")]
        HU,

        [Description("Iceland")]
        IS,

        [Description("India")]
        IN,

        [Description("Indonesia")]
        ID,

        [Description("Iran")]
        IR,

        [Description("Iraq")]
        IQ,

        [Description("Ireland")]
        IE,

        [Description("Israel")]
        IL,

        [Description("Italy")]
        IT,

        [Description("Jamaica")]
        JM,

        [Description("Japan")]
        JP,

        [Description("Kenya")]
        KE,

        [Description("Korea")]
        KP_KR,

        [Description("Libya")]
        LY,

        [Description("Lithuania")]
        LT,

        [Description("Luxembourg")]
        LU,

        [Description("Macedonia")]
        MK,

        [Description("Madagascar")]
        MG,

        [Description("Mali")]
        ML,

        [Description("Malta")]
        MT,

        [Description("Martinique")]
        MQ,

        [Description("Mexico")]
        MX,

        [Description("Monaco")]
        MC,

        [Description("Mongolia")]
        MN,

        [Description("Morocco")]
        MA,

        [Description("Nepal")]
        NP,

        [Description("Netherlands")]
        NL,

        [Description("New Caledonia")]
        NC,

        [Description("New Zealand")]
        NZ,

        [Description("Nigeria")]
        NG,

        [Description("Norway")]
        NO,

        [Description("Peru")]
        PE,

        [Description("Philippines")]
        PH,

        [Description("Poland")]
        PL,

        [Description("Portugal")]
        PT,

        [Description("Qatar")]
        QA,

        [Description("Russian Federation")]
        RU,

        [Description("Reunion")]
        RE,

        [Description("Saudi Arabia")]
        SA,

        [Description("Senegal")]
        SN,

        [Description("Serbia")]
        RS,

        [Description("Slovakia")]
        SK,

        [Description("Slovenia")]
        SI,

        [Description("South Africa")]
        ZA,

        [Description("Spain")]
        ES,

        [Description("Sweden")]
        SE,

        [Description("Switzerland")]
        CH,

        [Description("Syria")]
        SY,

        [Description("Taiwan")]
        TW,

        [Description("Thailand")]
        TH,

        [Description("Tunisia")]
        TN,

        [Description("Turkey")]
        TR,

        [Description("Uganda")]
        UG,

        [Description("Ukraine")]
        UA,

        [Description("United Kingdom")]
        GB,

        [Description("United States")]
        US,

        [Description("Venezuela")]
        VE,

        [Description("Viet Nam")]
        VN,

        [Description("Yemen")]
        YE,

        [Description("Zimbabwe")]
        ZW,
    }

    public static class EnumHelpers
    {
        public static string ToDescriptionString(this Nationality val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
