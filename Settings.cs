using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace VaderConsulting.Helper
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.xml";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }

    public class Settings : AppSettings<Settings>
    {
        public string SystemCenter_SCOMServer = "DTFSCOMTST001";
        public string iServer_DatabaseConnectionString = "Server=iServerDB;Database=iServerDB;Trusted_Connection=true;";
        public string iServer23307_RelationshipsQuery =  "";
/*@"SELECT r.RelationshipId 
      ,r.FromObjectId 
      ,r.ToObjectId 
      ,o2.ObjectName AS Service 
      ,v.ObjectName AS ComponentName 
      ,r.RelationReason AS Description 
      ,v.TypeId 
      ,v.ObjectDescription AS ComponentDescription 
      ,v.ObjectVersionId AS VersionID 
      --,a.AttributeValue AS ShowInRunbook 
      ,c.AttributeValue AS RunbookOrder
      ,m.AttributeValue AS Mandatory 
FROM dbo.Relation AS r 
LEFT OUTER JOIN dbo.vwObject AS v ON r.ToObjectId = v.ObjectID 
LEFT OUTER JOIN dbo.Object AS o2 ON r.FromObjectId = o2.ObjectID 
LEFT OUTER JOIN RelationType rt ON r.RelationTypeId = rt.RelationTypeID 
LEFT OUTER JOIN Timestamp t ON t.TimestampId = r.TimestampId 
LEFT OUTER JOIN Object o ON r.RelationshipId = o.ObjectId 
--LEFT OUTER JOIN AttributeValueBigInt a ON a.ObjectId = v.ObjectID AND a.AttributeId = '#SHOWINRUNBOOK#' AND a.VersionId = v.ObjectVersionId 
LEFT OUTER JOIN AttributeValueBigInt c ON c.ObjectId = v.ObjectID AND c.AttributeId = '#RUNBOOKORDER#' AND c.VersionId = v.ObjectVersionId 
LEFT OUTER JOIN AttributeValueBigInt m ON m.ObjectId = o.ObjectID AND m.AttributeId = '#MANDATORY#'
WHERE (r.FromObjectId LIKE '#OBJECTID#') AND (r.DeleteFlag = 0)";*/
        public string iServer23307_GetServerDetailsQuery = @"SELECT n.ObjectID
     , n.Server
     , avt.AttributeValue AS Site 
     , Stream.AttributeValue AS Stream
     , InScope.AttributeValue AS InScope
     , Virtual.AttributeValue AS Virtual
FROM dbo.AttributeValueText AS avt 
INNER JOIN ( SELECT DISTINCT o.ObjectID
                           , o.ObjectName        AS Server
                           , o.ObjectDescription AS Description
                           , v.VersionNo
                           , v.VersionID 
                           , o.ObjectVersionId
             FROM dbo.vwObject AS o INNER JOIN ( SELECT ObjectID
                                                      , MAX(CurrentVersionNo) AS VersionNo
                                                      , ID AS VersionID 
                                                 FROM dbo.Version AS v 
                                                 GROUP BY ObjectID, ID ) AS v ON o.ObjectVersionId = v.VersionID 
             WHERE (o.LibraryId = '00000000-0000-0000-0000-000000000000') 
               AND (o.IsDeleted = 0) 
               AND (o.TypeId = #SERVERID#) 
           ) AS n ON avt.AttributeId = '#PHYSICALSITE#' AND avt.ObjectId = n.ObjectID AND n.VersionID = avt.VersionId 
LEFT JOIN AttributeValueText Stream    ON Stream.VersionId  = n.ObjectVersionId AND Stream.AttributeId   = '#STREAM#'
LEFT JOIN AttributeValueBigInt InScope ON InScope.VersionId = n.ObjectVersionId AND InScope.AttributeId  = '#INSCOPE#'
LEFT JOIN AttributeValueBigInt Virtual ON Virtual.VersionId = n.ObjectVersionId AND Virtual.AttributeId  = '#VIRTUAL#'
ORDER BY SERVER";
    }
}

