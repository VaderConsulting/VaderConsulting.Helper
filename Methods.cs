using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Windows.Forms;

namespace VaderConsulting.Helper
{
    public static class Methods
    {
        public static string ToHex(this string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
                sb.AppendFormat("0x{0:X2} ", (int)c);
            return sb.ToString().Trim();
        }

        public static string ToHex(this byte input)
        {
            string Result = string.Format("{0:X2}", input);
            return Result;
        }

        public static bool IsGroupMember(string GroupName)
        {
            string CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            PrincipalContext Context = null;

            if (GroupName != "")
            {

                try
                {
                    Context = new PrincipalContext(ContextType.Domain);

                    // Remove domain name
                    CurrentUserName = CurrentUserName.Split('\\')[1];

                    // find the group in question
                    GroupPrincipal ADGroup = GroupPrincipal.FindByIdentity(Context, GroupName);

                    if (ADGroup != null)
                    {
                        // iterate over members
                        foreach (Principal p in ADGroup.GetMembers())
                        {
                            UserPrincipal User = p as UserPrincipal;

                            if (User != null)
                            {
                                if (CurrentUserName.ToUpper() == User.SamAccountName.ToUpper())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }

            return false;
        }

        public static string ReplaceTags(string InputString, System.DateTime InputDate, bool ConvertToUppercase)
        {
            string OutputString = InputString;
            Int16 QuarterNumber = 1;
            Int16 TaxQuarterNumber = 1;
            char PadCharacter = Convert.ToChar("0");

            // TODO:  When extra is required, this has to be a small scripting engine

            // This part allows us to pass through variables to other utilities\applications
            //OutputString = OutputString.Replace("##", "!!")

            try
            {
                // This does not work for the Local System account
                OutputString = OutputString.Replace("#DESKTOP#", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            }
            catch (Exception ex)
            {
                OutputString = OutputString.Replace("#DESKTOP#", "");
            }

            try
            {
                // This does not work for the Local System account
                OutputString = OutputString.Replace("#MYDOCS#", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }
            catch (Exception ex)
            {
                OutputString = OutputString.Replace("#MYDOCS#", "");
            }

            try
            {
                // This does not work for the Local System account
                OutputString = OutputString.Replace("#MYMUSIC#", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            }
            catch (Exception ex)
            {
                OutputString = OutputString.Replace("#MYMUSIC#", "");
            }

            try
            {
                // This does not work for the Local System account
                OutputString = OutputString.Replace("#MYPICS#", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            }
            catch (Exception ex)
            {
                OutputString = OutputString.Replace("#MYPICS#", "");
            }

            try
            {
                // This does not work for the Local System account
                OutputString = OutputString.Replace("#PROGRAMS#", Environment.GetFolderPath(Environment.SpecialFolder.Programs));
            }
            catch (Exception ex)
            {
                OutputString = OutputString.Replace("#PROGRAMS#", "");
            }

            System.Guid NewGUID = System.Guid.NewGuid();

            OutputString = OutputString.Replace("#GUID#", NewGUID.ToString());
            OutputString = OutputString.Replace("#ALLUSERSAPPDATA#", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            OutputString = OutputString.Replace("#APPDATA#", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            OutputString = OutputString.Replace("#PROGRAMFILES#", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            OutputString = OutputString.Replace("#TEMP#", System.IO.Path.GetTempPath());
            OutputString = OutputString.Replace("#HOUR-1#", DateAndTime.DateAdd(DateInterval.Hour, -1, InputDate).Hour.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#HOUR#", InputDate.Hour.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#MINUTE-1#", DateAndTime.DateAdd(DateInterval.Minute, -1, InputDate).Minute.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#MINUTE#", InputDate.Minute.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#SECOND#", InputDate.Second.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#DAY-1#", DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#DAY#", InputDate.Day.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#DATE#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#DATE2#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#DATE4#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#DATE-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#DATE2-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#DATE4-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#DATE+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#DATE2+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#DATE4+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#TODAY#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#TODAY2#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#TODAY4#", InputDate.Day.ToString().PadLeft(2, PadCharacter) + InputDate.Month.ToString().PadLeft(2, PadCharacter) + InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#TOMORROW#", DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#TOMORROW2#", DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#TOMORROW4#", DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YESTERDAY#", DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YESTERDAY2#", DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#YESTERDAY4#", DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Day.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter) + DateAndTime.DateAdd(DateInterval.Day, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#MONTH-1#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Month.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#MONTHNAME-1#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).ToString("MMMM"));
            OutputString = OutputString.Replace("#MONTHNAME#", InputDate.ToString("MMMM"));
            OutputString = OutputString.Replace("#MONTH#", InputDate.Month.ToString().PadLeft(2, PadCharacter));
            OutputString = OutputString.Replace("#YEAR-1#", DateAndTime.DateAdd(DateInterval.Year, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR#", InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR4-1#", DateAndTime.DateAdd(DateInterval.Year, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR4#", InputDate.Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR2-1#", DateAndTime.DateAdd(DateInterval.Year, -1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#YEAR2#", InputDate.Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#YEAR-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR2-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#YEAR4-1MONTH#", DateAndTime.DateAdd(DateInterval.Month, -1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#YEAR2+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().Substring(2, 2));
            OutputString = OutputString.Replace("#YEAR4+1MONTH#", DateAndTime.DateAdd(DateInterval.Month, 1, InputDate).Year.ToString().PadLeft(4, PadCharacter));
            OutputString = OutputString.Replace("#USERNAME#", Environment.UserName.Replace("\\", "_"));
            OutputString = OutputString.Replace("#COMPUTERNAME#", Environment.MachineName);
            OutputString = OutputString.Replace("#APPPATH#", System.Reflection.Assembly.GetExecutingAssembly().Location);
            OutputString = OutputString.Replace("#ASSEMBLY#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            OutputString = OutputString.Replace("#COMPANY#", ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company);
            OutputString = OutputString.Replace("#PRODUCT#", ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute), false)).Product);
            OutputString = OutputString.Replace("#APPMAJORVERSION#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());
            OutputString = OutputString.Replace("#APPMINORVERSION#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString());
            OutputString = OutputString.Replace("#APPREVISION#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString());
            OutputString = OutputString.Replace("#APPMINORREVISION#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.MinorRevision.ToString());
            OutputString = OutputString.Replace("#APPBUILD#", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());

            switch (InputDate.Month)
            {
                case 1:
                case 2:
                case 3:
                    QuarterNumber = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    QuarterNumber = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    QuarterNumber = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    QuarterNumber = 4;
                    break;
            }
            OutputString = OutputString.Replace("#QUARTER#", QuarterNumber.ToString());
            OutputString = OutputString.Replace("#QUARTER2#", QuarterNumber.ToString()).PadLeft(2, PadCharacter);

            switch (InputDate.Month)
            {
                case 7:
                case 8:
                case 9:
                    TaxQuarterNumber = 1;
                    break;
                case 10:
                case 11:
                case 12:
                    TaxQuarterNumber = 2;
                    break;
                case 1:
                case 2:
                case 3:
                    TaxQuarterNumber = 3;
                    break;
                case 4:
                case 5:
                case 6:
                    TaxQuarterNumber = 4;
                    break;
            }
            OutputString = OutputString.Replace("#TAXQUARTER#", TaxQuarterNumber.ToString());
            OutputString = OutputString.Replace("#TAXQUARTER2#", TaxQuarterNumber.ToString()).PadLeft(2, PadCharacter);

            if (ConvertToUppercase)
                OutputString = OutputString.ToUpper();

            return OutputString;
        }

        public static void SetComboBoxToTextIndex(ComboBox TheComboBox, string ItemText)
        {
            for (int i = 0 ; i < TheComboBox.Items.Count - 1 ; i++)
            {
                string ThisColour = TheComboBox.Items[i].ToString();

                if (ItemText == ThisColour)
                {
                    TheComboBox.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
