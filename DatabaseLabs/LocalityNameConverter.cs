using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;
using Domain;
using System.Collections.ObjectModel;

namespace Lab2
{
    class LocalityNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                try
                {
                    ObservableCollection<string> result = new();
                    foreach (var elem in (ObservableCollection<LocalityName>)value)
                    {
                        result.Add(elem.Name);
                    }
                    return result;
                }
                catch (Exception ex) 
                {
                    return new ObservableCollection<string>();
                }
            }
            //return ((PlacementAlongTheRoad)value).Name;
            else
                return new ObservableCollection<string>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
