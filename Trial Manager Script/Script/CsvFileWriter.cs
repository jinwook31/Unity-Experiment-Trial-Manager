using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CsvFileWriter : CsvFileCommon, IDisposable
{
	// Private members
	private StreamWriter Writer;
	private string OneQuote = null;
	private string TwoQuotes = null;
	private string QuotedFormat = null;
	
	/// <summary>
	/// Initializes a new instance of the CsvFileWriter class for the
	/// specified stream.
	/// </summary>
	/// <param name="stream">The stream to write to</param>
	public CsvFileWriter(Stream stream)
	{
		Writer = new StreamWriter(stream);
	}
	
	/// <summary>
	/// Initializes a new instance of the CsvFileWriter class for the
	/// specified file path.
	/// </summary>
	/// <param name="path">The name of the CSV file to write to</param>
	public CsvFileWriter(string path)
	{
		Writer = new StreamWriter(path);
	}
	
	/// <summary>
	/// Writes a row of columns to the current CSV file.
	/// </summary>
	/// <param name="columns">The list of columns to write</param>
	public void WriteRow(List<string> columns)
	{
		// Verify required argument
		if (columns == null)
			throw new ArgumentNullException("columns");
		
		// Ensure we're using current quote character
		if (OneQuote == null || OneQuote[0] != Quote)
		{
			OneQuote = String.Format("{0}", Quote);
			TwoQuotes = String.Format("{0}{0}", Quote);
			QuotedFormat = String.Format("{0}{{0}}{0}", Quote);
		}
		
		// Write each column
		for (int i = 0; i < columns.Count; i++)
		{
			// Add delimiter if this isn't the first column
			if (i > 0)
				Writer.Write(Delimiter);
			// Write this column
			if (columns[i].IndexOfAny(SpecialChars) == -1)
				Writer.Write(columns[i]);
			else
				Writer.Write(QuotedFormat, columns[i].Replace(OneQuote, TwoQuotes));
		}
		Writer.WriteLine();
	}
	
	// Propagate Dispose to StreamWriter
	public void Dispose()
	{
		Writer.Dispose();
	}
}
