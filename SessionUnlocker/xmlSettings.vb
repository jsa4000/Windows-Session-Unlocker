'*********************************************************************
' MODULE: XML Settings
' FILENAME: xmlSettings.vb
' AUTHOR: Teodoro Seoane
' DEPENDENCIES: xmlFunctions.vb
'
' DESCRIPTION:
' This module creates and reads an xml file for configuration issues.
' The structure is:
'   <settings>
'       <group1>
'           <subgroup1>value1</subgroup1> 
'           <subgroup2>value2</subgroup2> 
'       </group1>
'       <group2>
'           <subgroup1>value1</subgroup1> 
'           <subgroup2>value2</subgroup2> 
'       </group2>
'   </settings>
' 
' MODIFICATION HISTORY:
' 1.0.0 06-Oct-2008 TSE - Initial Version
' 1.0.1 03-Nov-2008 TSE - Folder for xml file is created if doesn't exist
' 2.0.0 06-Nov-2008 TSE - xml functions in its own module, xmlFunctions.vb
' 2.1.0 12-Dec-2008 TSE - Uses new function in xmlFunctions.vb 1.1.0
'*********************************************************************
Module xmlSettings

    Public Sub SaveXMLSetting(ByVal Group As String, ByVal SubGroup As String, _
    ByVal SubGroupValue As String, ByVal XML_File As String)
        Dim DocXML As New Xml.XmlDocument
        DocXML = Create_XML_File(XML_File, "settings")
        CreateUniqueNode(DocXML, "//settings", Group)
        CreateUniqueNode(DocXML, "//settings" & "/" & Group, SubGroup, SubGroupValue)
        DocXML.Save(XML_File)
    End Sub

    Public Function ReadXMLSetting(ByVal Group As String, ByVal SubGroup As String, _
    ByVal DefaultValue As String, ByVal XML_File As String) As String

        ReadXMLSetting = DefaultValue
        If Not My.Computer.FileSystem.FileExists(XML_File) Then Exit Function
        Dim DocXML As New Xml.XmlDocument
        Dim MainNode As Xml.XmlNode
        Try
            DocXML.Load(XML_File)
        Catch ex As Exception
            'xml file doesn't exist or corrupted structure
            Exit Function
        End Try
        MainNode = DocXML.SelectSingleNode("//settings" & "/" & Group & "/" & SubGroup)
        If Not MainNode Is Nothing Then
            ReadXMLSetting = MainNode.InnerText
        End If
    End Function

End Module
