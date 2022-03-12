using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ADIF.NET {
  public class CSVAdapter {

    CsvReader reader;
    string contents;

    public CSVAdapter(string filename, char delimiter, string newLine, char quotedBy)
    {
      reader = new CsvReader(new CsvConfig(delimiter, newLine, quotedBy));

      var fi = new FileInfo(filename);
      if (fi.Exists)
        contents = File.ReadAllText(filename);
    }




    class CsvConfig {
      public char Delimiter { get; private set; }
      public string NewLineMark { get; private set; }
      public char QuotationMark { get; private set; }

      public CsvConfig(char delimiter, string newLineMark, char quotationMark)
      {
        Delimiter = delimiter;
        NewLineMark = newLineMark;
        QuotationMark = quotationMark;
      }

      // useful configs

      public static CsvConfig Default
      {
        get { return new CsvConfig(',', "\r\n", '\"'); }
      }
    }

    class CsvReader {
      private CsvConfig m_config;

      public CsvReader(CsvConfig config = null)
      {
        if (config == null)
          m_config = CsvConfig.Default;
        else
          m_config = config;
      }

      public IEnumerable<string[]> Read(string csvFileContents)
      {
        using (var reader = new StringReader(csvFileContents))
        {
          while (true)
          {
            string line = reader.ReadLine();
            if (line == null)
              break;
            yield return ParseLine(line);
          }
        }
      }

      private string[] ParseLine(string line)
      {
        var result = new Stack<string>();

        int i = 0;
        while (true)
        {
          string cell = ParseNextCell(line, ref i);
          if (cell == null)
            break;
          result.Push(cell);
        }

        // remove last elements if they're empty
        while (string.IsNullOrEmpty(result.Peek()))
        {
          result.Pop();
        }

        var resultAsArray = result.ToArray();
        Array.Reverse(resultAsArray);
        return resultAsArray;
      }

      // returns iterator after delimiter or after end of string
      private string ParseNextCell(string line, ref int i)
      {
        if (i >= line.Length)
          return null;

        if (line[i] != m_config.QuotationMark)
          return ParseNotEscapedCell(line, ref i);
        else
          return ParseEscapedCell(line, ref i);
      }

      // returns iterator after delimiter or after end of string
      private string ParseNotEscapedCell(string line, ref int i)
      {
        var sb = new StringBuilder();
        while (true)
        {
          if (i >= line.Length) // return iterator after end of string
            break;
          if (line[i] == m_config.Delimiter)
          {
            i++; // return iterator after delimiter
            break;
          }
          sb.Append(line[i]);
          i++;
        }
        return sb.ToString();
      }

      // returns iterator after delimiter or after end of string
      private string ParseEscapedCell(string line, ref int i)
      {
        i++; // omit first character (quotation mark)
        var sb = new StringBuilder();
        while (true)
        {
          if (i >= line.Length)
            break;
          if (line[i] == m_config.QuotationMark)
          {
            i++; // we're more interested in the next character
            if (i >= line.Length)
            {
              // quotation mark was closing cell;
              // return iterator after end of string
              break;
            }
            if (line[i] == m_config.Delimiter)
            {
              // quotation mark was closing cell;
              // return iterator after delimiter
              i++;
              break;
            }
            if (line[i] == m_config.QuotationMark)
            {
              // it was doubled (escaped) quotation mark;
              // do nothing -- we've already skipped first quotation mark
            }

          }
          sb.Append(line[i]);
          i++;
        }

        return sb.ToString();
      }
    }

  }
}
