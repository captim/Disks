using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Disks
{
    class ConvertDataInDoc
    {
        Application wordApp = null;
        Document wordDoc = null;
        string filePath = "";
        public void ConvertFlightListInDoc(List<Disk> selYList, List<Disk> selXYList)
        {
            try
            {
                filePath = Environment.CurrentDirectory.ToString();
                wordApp = new Application();
                wordDoc = wordApp.Documents.Add(filePath + "\\Результати пошуку інформації.dotm");
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex, "Помістіть файл Результати пошуку інформації.dotm"
                    + char.ConvertFromUtf32(13) + "у каталог із exe-файлом програми і повторіть збереження",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            ReplaceText(MainWindow.selectedCompany, "[Y]");
            ReplaceText(selYList, 1);
            ReplaceText(MainWindow.selectedType, "[X]");
            ReplaceText(selXYList, 2);
            try
            {
                wordDoc.Save();
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex, "Помилка збереження відібраних даних",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

            }
        }

        private void ReplaceText(string textToReplace, string replacedText)
        {
            Object missing = Type.Missing;
            Range selText;
            selText = wordDoc.Range(wordDoc.Content.Start, wordDoc.Content.End);
            Find find = wordApp.Selection.Find;
            find.Text = replacedText;
            find.Replacement.Text = textToReplace;
            Object wrap = WdFindWrap.wdFindContinue;
            Object replace = WdReplace.wdReplaceAll;


            find.Execute(FindText: Type.Missing,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                MatchSoundsLike: missing,
                MatchAllWordForms: false,
                Forward: true,
                Wrap: wrap,
                Format: false,
                ReplaceWith: missing, Replace: replace);
        }
        private void ReplaceText(List<Disk> selectedList, int numTable)
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                wordDoc.Tables[numTable].Rows.Add();
                wordDoc.Tables[numTable].Cell(2 + i, 1).Range.Text =
                    selectedList[i].code;
                wordDoc.Tables[numTable].Cell(2 + i, 2).Range.Text =
                    selectedList[i].title;
                if (numTable == 2)
                {
                    wordDoc.Tables[numTable].Cell(2 + i, 3).Range.Text =
                        selectedList[i].type;
                }
            }
        }
    }
}
