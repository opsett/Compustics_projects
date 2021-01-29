using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media;

namespace DynamicDatagridColumns.Views
{
    internal class SheetColors: Collection<Color>, IColorsRepository
    {
        public SheetColors()
        {
            foreach (PropertyInfo property in typeof(Colors).GetProperties())
            {
                Add((Color)property.GetValue(null, null));
            }
        }



    }
}
