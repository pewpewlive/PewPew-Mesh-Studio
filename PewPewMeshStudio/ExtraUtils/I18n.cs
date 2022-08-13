using GetText;
using System.Globalization;

namespace PewPewMeshStudio.ExtraUtils
{
    public static class I18n
    {
        public static string[] rangeGlyphs =
        {
            "ĄČĘĖĮŠŲŪŽąčęėįšųūž„“", // Lithuanian
            "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθιγκλμνξοπρστυφχψωάέύίόώήϋϊΰΐΈΎΉΏΌ", // Greek
            "$ഀ$ഁ$ം$ഃഄഅആഇഈഉഊഋഌഎഏഐഒഓഔകഖഗഘങചഛജഝഞടഠഡഢണതഥദധനഩപഫബഭമയരറലളഴവശഷസഹഺ$഻$഼ഽ$ാ$ി$ീ$ു$ൂ$ൃ$ൄ$െ$േ$ൈ$ൊ$ോ$ൌ$്ൎ൥ൔൕൖ$ൗ൘൙൚൛൜൝൞ൟൠൡ$ൢ$ൣ൦൧൨൩൪൫൬൭൮൯൰൱൲൳൴൵൶൷൸൹ൺൻർൽൾൿ" // Malayalam
        };
        public static ICatalog c = new Catalog("Translation", ".\\locale", new CultureInfo("lt"));
    }
}
