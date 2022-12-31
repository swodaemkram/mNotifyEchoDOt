Imports System
Imports System.IO
Imports System.Net

Module Module1

    '****************************************************************************************************
    '************************************** Command Line arguments should look like 
    '************************************** C:\mNotify URL "Message" AuthKey
    '************************** Since there will be so much variaty little argument checking will be done
    '*****************************************************************************************************

    Sub Main(args As String())
        Dim arguments = Environment.GetCommandLineArgs()
        On Error Resume Next

        'Debug Console.WriteLine(arguments(1)) 'Command Line Argument 1
        'Debug Console.WriteLine(arguments(2)) 'Command Line Argument 2
        'Debug Console.WriteLine(arguments(3)) 'Command Line Argument 3

        arguments(2) = arguments(2).Replace(" ", "%20")

        Dim CompletURL As String = arguments(1) & arguments(2) & "&" & arguments(3)
        Console.WriteLine(CompletURL)

        Dim ReturnString As String = processCCRequest(CompletURL)

        'Console.WriteLine(ReturnString)
    End Sub
    'Public Function processCCRequest(ByVal strRequest As String) As String Changed this line to the line below 
    Public Function processCCRequest(CompleteURL As String) As String
        'declare the web request object and set its path to the  API


        Dim ThisRequest As WebRequest = WebRequest.Create(CompleteURL)

        'configure web request object attributes
        ThisRequest.ContentType = "application/x-www-form-urlencoded"
        ThisRequest.Method = "POST"

        'encode the request
        Dim Encoder As New System.Text.ASCIIEncoding
        'Dim BytesToSend As Byte() = Encoder.GetBytes(strRequest) I Changed the line below for simplicity
        Dim BytesToSend As Byte() = Encoder.GetBytes("")

        'declare the text stream and send the request to the API
        Dim StreamToSend As Stream = ThisRequest.GetRequestStream
        StreamToSend.Write(BytesToSend, 0, BytesToSend.Length)
        StreamToSend.Close()

        ''ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        ''allows for validation of SSL conversations
        ''ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf)
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ''| SecurityProtocolType.Tls11 | SecurityProtocolType.Tls
        ''var(response = WebRequest.Create("https://www.howsmyssl.com/").GetResponse())
        ''var(body = New StreamReader(response.GetResponseStream()).ReadToEnd())


        'Catch the response from the webrequest object
        Dim TheirResponse As HttpWebResponse = ThisRequest.GetResponse

        Dim sr As New StreamReader(TheirResponse.GetResponseStream)
        Dim strResponse As String = sr.ReadToEnd

        'Out put the string to a message box - application should parse the request instead
        ' MsgBox(strResponse)

        sr.Close()
        Return strResponse
    End Function


End Module
