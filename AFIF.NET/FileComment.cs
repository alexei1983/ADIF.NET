
namespace ADIF.NET {
  public class FileComment {

    public int LineNumber { get; set; }

    public string Text { get; set; }

    public FileComment() { }

    public FileComment(int lineNumber, string text) {
      this.LineNumber = lineNumber;
      this.Text = text;
      }

    }
  }
