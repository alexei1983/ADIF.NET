using System;
using System.Text;
using System.Net;
using HtmlAgilityPack;


namespace ADIF.NET.Helpers {
  public class GridSquareHelper {

    enum LookupType {
      Undefined = 0,
      Address = 1,
      Call = 2,
      GridSquare = 3
      }

    const string BaseUrl = "http://www.levinecentral.com/ham/grid_square.php";

    public string GetGridSquareFromAddress(string address) {
      return GetGridSquare(address, LookupType.Address);
      }

    public string GetGridSquareFromCall(string call) {
      return GetGridSquare(call, LookupType.Call);
      }

    public string GetGridSquareFromGridSquare(string gridSquare) {
      return GetGridSquare(gridSquare, LookupType.GridSquare);
      }

    string GetGridSquare(string value, LookupType type) {

      if (type == LookupType.Undefined)
        type = LookupType.Address;

      value = value ?? string.Empty;

      var grid = string.Empty;

      var url = BaseUrl;

      switch (type) {
      case LookupType.Address:
        url = $"{url}?Address=";
        break;

      case LookupType.Call:
        url = $"{url}?Call=";
        break;

      case LookupType.GridSquare:
        url = $"{url}?Grid=";
        break;

      default:
        throw new InvalidOperationException("Invalid grid square lookup type.");
        }

      url = $"{url}{value.UrlEncode()}";

      var html = string.Empty;

      // grab the HTML from the service
      using (var client = new WebClient()) {
        html = client.DownloadString(url);
        }

      var doc = new HtmlDocument();
      doc.LoadHtml(html);
      var node = doc.DocumentNode.SelectSingleNode("//html/body/center/p/font/b");

      if (node != null && node.InnerHtml != null)
        grid = node.InnerHtml.Trim();
        
      return grid;
      }

    public string[] GridSquaresFromList(string gridSquareList, char delimiter = Values.Comma) {

      if (string.IsNullOrWhiteSpace(gridSquareList))
        return null;

      var gridSquares = gridSquareList.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

      return gridSquares;
      }
    }
  }
