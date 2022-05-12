public abstract class CsvFileCommon
{
	/// <summary>
	/// These are special characters in CSV files. If a column contains any
	/// of these characters, the entire column is wrapped in double quotes.
	/// </summary>
	protected char[] SpecialChars = new char[] { ',', '"', '\r', '\n' };
	
	// Indexes into SpecialChars for characters with specific meaning
	private const int DelimiterIndex = 0;
	private const int QuoteIndex = 1;
	
	/// <summary>
	/// Gets/sets the character used for column delimiters.
	/// </summary>
	public char Delimiter
	{
		get { return SpecialChars[DelimiterIndex]; }
		set { SpecialChars[DelimiterIndex] = value; }
	}
	
	/// <summary>
	/// Gets/sets the character used for column quotes.
	/// </summary>
	public char Quote
	{
		get { return SpecialChars[QuoteIndex]; }
		set { SpecialChars[QuoteIndex] = value; }
	}
}
