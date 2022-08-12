using GetText;
using System.Globalization;

namespace PewPewMeshStudio.ExtraUtils
{
    public static class I18n
    {
        public static ICatalog c = new Catalog("Translation", ".\\locale", new CultureInfo("lt-LT"));
    }
}
