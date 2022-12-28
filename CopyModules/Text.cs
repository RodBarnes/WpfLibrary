using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CopyModules
{
    public class Text
    {
		// This was used in TSC Library to be able to identify text size on the display in order to use that
		// to size the appopriate text when priting.
		// Code to demonstrate how to get the physical width and height of a text as it will be displayed
		// This will only work in code that will use the display; e.g, a WPF app for running on a desktop.
		// Since web services do not have a display, the necessary framework methods are not available.

		public static void ShowTextSize(object obj)
		{
			if ((TSClib.ElementType)ElementType == TSClib.ElementType.WindowsText)
			{
				var size = GetDisplayTextSize(ContentText, ContentFontName, TSC.WindowsStyleList[ContentStyle].Value, ContentHeight);
				MessageBox.Show($"width={size.Width}, height={size.Height}", "Printer Font Size");
			}
		}

		/// <summary>
		/// Determine the width and height in points (1/72") of a specified text.
		/// </summary>
		/// <param name="text">text to be examined</param>
		/// <param name="font">name of the font; e.g., 'Arial'</param>
		/// <param name="style">name of the style, e.g., 'Normal', 'Italic', etc.</param>
		/// <param name="height">height of the font in points</param>
		/// <returns></returns>
		private static Size GetDisplayTextSize(string text, string font, string style, int height)
		{
			// Convert from points (1/72") to device-independent units (1/96")
			var fontSize = (double)height * 72 / 96;

			var fontFamily = new FontFamily(font);
			var fontStretch = FontStretches.Normal;
			var fontStyle = System.Windows.FontStyles.Normal; ;
			var fontWeight = FontWeights.Regular;
			switch (style)
			{
				case "Italic":
					fontStyle = System.Windows.FontStyles.Italic;
					fontWeight = FontWeights.Regular;
					break;
				case "Bold":
					fontStyle = System.Windows.FontStyles.Normal;
					fontWeight = FontWeights.Bold;
					break;
				case "BoldItalic":
					fontStyle = System.Windows.FontStyles.Italic;
					fontWeight = FontWeights.Bold;
					break;
			}

			var typeFace = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

			var formattedText = new FormattedText(
				ContentText,
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				typeFace,
				fontSize,
				Brushes.Black,
				new NumberSubstitution(),
				TextFormattingMode.Display);

			// Convert from device-independent units to points
			var szWidth = formattedText.Width * 96 / 72;
			var szHeight = formattedText.Height * 96 / 72;
			var size = new Size(szWidth, szHeight);

			return size;
		}
	}
}
