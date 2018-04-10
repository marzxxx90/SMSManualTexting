Module formSwitch
    
    Friend Enum FormName As Integer
        devForm = 0
        frmMTSend = 1 'Money Transfer
        frmPawning = 2
        frmInsurance = 3
        frmMTReceive = 4
        frmDollar = 5
        frmPawnItem = 6
        frmDollarSimple = 7
        frmMoneyExchange = 8
        frmAdminPanel = 9

        frmPawningV2_Client = 10
        frmPawningV2_Specs = 11
        frmPawningV2_Claimer = 12
        frmPawningV2_SpecsValue = 13
        frmPawningV2_InterestScheme = 14

        Coi = 15
        layAway = 16
        layAwayExist = 17

        frmWesternExchangeSender = 18
        frmWesternExchangeReceiver = 19
        frmWesternCurrency = 20

        POSClient = 21
    End Enum

    'Friend Sub ReloadFormFromSearch(ByVal gotoForm As FormName, ByVal cus As Customer)
    '    Select Case gotoForm
    '        Case FormName.frmMTSend
    '            frmMoneyTransfer.LoadSenderInfo(cus)
    '        Case FormName.frmInsurance
    '            frmInsurance.LoadHolder(cus)
    '        Case FormName.frmMTReceive
    '            frmMoneyTransfer.LoadReceiverInfo(cus)
    '        Case FormName.frmMoneyExchange
    '            frmmoneyexchange.LoadCustomer(cus)

    '        Case FormName.frmPawningV2_Client
    '            frmPawningItemNew.LoadClient(cus)
    '        Case FormName.frmPawningV2_Claimer
    '            frmPawningItemNew.LoadCliamer(cus)

    '        Case FormName.layAway
    '            frmLayAway.LoadClient(cus)
    '        Case FormName.layAwayExist
    '            frmAddCustomer.LoadClient(cus)

    '        Case FormName.frmWesternExchangeSender
    '            frmWesternExchange.LoadSenderInfo(cus)
    '        Case FormName.frmWesternExchangeReceiver
    '            frmWesternExchange.LoadReceiverInfo(cus)

    '        Case FormName.POSClient
    '            frmSales.LoadClient(cus)
    '    End Select
    'End Sub
End Module