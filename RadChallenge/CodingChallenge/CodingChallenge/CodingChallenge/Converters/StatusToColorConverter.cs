using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CodingChallenge.Models;

namespace CodingChallenge.Converters
{
    /// <summary>
    /// Converts CheckStatus enum to corresponding brush color for UI display.
    /// </summary>
    public class StatusToColorConverter : IValueConverter
    {
        // Catppuccin Mocha palette colors
        public static readonly SolidColorBrush PassBrush = new(Color.FromRgb(166, 227, 161));    // Green
        public static readonly SolidColorBrush FailBrush = new(Color.FromRgb(243, 139, 168));    // Red
        public static readonly SolidColorBrush WarningBrush = new(Color.FromRgb(249, 226, 175)); // Yellow
        public static readonly SolidColorBrush NotRunBrush = new(Color.FromRgb(108, 112, 134)); // Gray

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckStatus status)
            {
                return status switch
                {
                    CheckStatus.Pass => PassBrush,
                    CheckStatus.Fail => FailBrush,
                    CheckStatus.Warning => WarningBrush,
                    _ => NotRunBrush
                };
            }
            return NotRunBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts CheckStatus enum to icon symbol (Unicode character).
    /// </summary>
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckStatus status)
            {
                return status switch
                {
                    CheckStatus.Pass => "✓",
                    CheckStatus.Fail => "✗",
                    CheckStatus.Warning => "⚠",
                    _ => "○"
                };
            }
            return "○";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts CheckStatus enum to text description.
    /// </summary>
    public class StatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckStatus status)
            {
                return status switch
                {
                    CheckStatus.Pass => "PASS",
                    CheckStatus.Fail => "FAIL",
                    CheckStatus.Warning => "WARN",
                    _ => "—"
                };
            }
            return "—";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts boolean to visibility (true = Visible, false = Collapsed).
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return System.Windows.Visibility.Visible;
            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

