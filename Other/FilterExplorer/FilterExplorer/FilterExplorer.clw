; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CBlurDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "FilterExplorer.h"
LastPage=0

ClassCount=17
Class1=CFilterExplorerApp
Class2=CFilterExplorerDoc
Class3=CFilterExplorerView
Class4=CMainFrame

ResourceCount=28
Resource1=IDD_ABOUTBOX
Class5=CAboutDlg
Resource2=CG_IDD_PROGRESS
Resource3=IDD_MEDIANBLUR
Class6=CProgressDlg
Class7=CBlurDlg
Resource4=IDD_FISHEYE
Class8=CEqIntensityDlg
Resource5=IDD_OIL
Class9=CFishEyeDlg
Resource6=IDD_FROSTGLASS
Class10=CHalftoneDlg
Resource7=IDD_RANDBLUR
Class11=CHueSatDlg
Resource8=IDD_BLUR
Class12=COilDlg
Resource9=IDD_HALFTONE
Class13=CRandBlurDlg
Resource10=IDD_EQINTENSITY
Class14=CConvolutionDlg
Resource11=IDD_CONVOLUTION
Class15=CFrostGlassDlg
Resource12=IDD_HUESAT
Class16=CMedianBlurDlg
Resource13=IDR_MAINFRAME
Class17=CRaindropsDlg
Resource14=IDD_RAINDROPS
Resource15=IDD_HALFTONE (English (U.S.))
Resource16=IDD_BLUR (English (U.S.))
Resource17=IDD_FROSTGLASS (English (U.S.))
Resource18=IDD_MEDIANBLUR (English (U.S.))
Resource19=IDD_ABOUTBOX (English (U.S.))
Resource20=IDD_EQINTENSITY (English (U.S.))
Resource21=IDD_FISHEYE (English (U.S.))
Resource22=IDR_MAINFRAME (English (U.S.))
Resource23=IDD_CONVOLUTION (English (U.S.))
Resource24=CG_IDD_PROGRESS (English (U.S.))
Resource25=IDD_HUESAT (English (U.S.))
Resource26=IDD_RANDBLUR (English (U.S.))
Resource27=IDD_OIL (English (U.S.))
Resource28=IDD_RAINDROPS (English (U.S.))

[CLS:CFilterExplorerApp]
Type=0
HeaderFile=FilterExplorer.h
ImplementationFile=FilterExplorer.cpp
Filter=N

[CLS:CFilterExplorerDoc]
Type=0
HeaderFile=FilterExplorerDoc.h
ImplementationFile=FilterExplorerDoc.cpp
Filter=N
LastObject=CFilterExplorerDoc

[CLS:CFilterExplorerView]
Type=0
HeaderFile=FilterExplorerView.h
ImplementationFile=FilterExplorerView.cpp
Filter=C
BaseClass=CScrollView
VirtualFilter=VWC
LastObject=ID_FILTER_RAINDROPS


[CLS:CMainFrame]
Type=0
HeaderFile=MainFrm.h
ImplementationFile=MainFrm.cpp
Filter=T
BaseClass=CFrameWnd
VirtualFilter=fWC
LastObject=CMainFrame




[CLS:CAboutDlg]
Type=0
HeaderFile=FilterExplorer.cpp
ImplementationFile=FilterExplorer.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=12
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352
Control12=IDC_STATIC,static,1342308352

[MNU:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_OPEN
Command2=ID_FILE_SAVE
Command3=ID_FILE_SAVE_AS
Command4=ID_APP_EXIT
Command5=ID_UNDO
Command6=ID_FILTER_GRAYSCALE_BYINTENSITY
Command7=ID_FILTER_GRAYSCALE_BYHUE
Command8=ID_FILTER_GRAYSCALE_BYSATURATION
Command9=ID_FILTER_HUESATURATION
Command10=ID_FILTER_EQUALIZEINTENSITY
Command11=ID_FILTER_BLUR
Command12=ID_FILTER_MEDIANBLUR
Command13=ID_FILTER_CONVOLUTION
Command14=ID_FILTER_FISHEYELENS
Command15=ID_FILTER_POLARCOORDINATES
Command16=ID_FILTER_OILPAINT
Command17=ID_FILTER_FROSTEDGLASS
Command18=ID_FILTER_RANDOMBLUR
Command19=ID_FILTER_RAINDROPS
Command20=ID_VIEW_TOOLBAR
Command21=ID_VIEW_STATUS_BAR
Command22=ID_APP_ABOUT
CommandCount=22

[ACL:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_UNDO
Command5=ID_EDIT_CUT
Command6=ID_EDIT_COPY
Command7=ID_EDIT_PASTE
Command8=ID_EDIT_UNDO
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_NEXT_PANE
Command13=ID_PREV_PANE
CommandCount=13

[TB:IDR_MAINFRAME]
Type=1
Class=?
Command1=ID_FILE_OPEN
Command2=ID_FILE_SAVE_AS
Command3=ID_UNDO
Command4=ID_APP_ABOUT
CommandCount=4

[DLG:IDD_BLUR]
Type=1
Class=CBlurDlg
ControlCount=5
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_RADIUS,edit,1350639744
Control4=IDC_STATIC,static,1342308352
Control5=IDC_CHECKFAST,button,1342242819

[DLG:CG_IDD_PROGRESS]
Type=1
Class=CProgressDlg
ControlCount=3
Control1=IDCANCEL,button,1342242817
Control2=CG_IDC_PROGDLG_PROGRESS,msctls_progress32,1350565888
Control3=CG_IDC_PROGDLG_PERCENT,static,1342308352

[CLS:CProgressDlg]
Type=0
HeaderFile=Dialogs.h
ImplementationFile=Dialogs.cpp
BaseClass=CDialog
LastObject=CG_IDC_PROGDLG_PERCENT

[CLS:CBlurDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
LastObject=IDC_RADIUS
VirtualFilter=dWC

[CLS:CEqIntensityDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=IDC_INTENSITY

[DLG:IDD_EQINTENSITY]
Type=1
Class=CEqIntensityDlg
ControlCount=4
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_INTENSITY,edit,1350631552
Control4=IDC_STATIC,static,1342308352

[DLG:IDD_FISHEYE]
Type=1
Class=CFishEyeDlg
ControlCount=11
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,button,1342177287
Control4=IDC_STATIC,button,1342177287
Control5=IDC_RADIO_ALONE,button,1342308361
Control6=IDC_RADIO_WHITE,button,1342177289
Control7=IDC_RADIO_BLACK,button,1342177289
Control8=IDC_CHECK_INVERSE,button,1342242819
Control9=IDC_SLIDER_CURVE,msctls_trackbar32,1342242840
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352

[CLS:CFishEyeDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CFishEyeDlg

[DLG:IDD_HALFTONE]
Type=1
Class=CHalftoneDlg
ControlCount=5
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_HALFTONES,edit,1350631552
Control4=IDC_STATIC,static,1342308352
Control5=IDC_CHECK_COLOR,button,1342242819

[CLS:CHalftoneDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=IDC_CHECK_COLOR

[DLG:IDD_HUESAT]
Type=1
Class=CHueSatDlg
ControlCount=12
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_HUE,msctls_trackbar32,1342242840
Control4=IDC_SAT,msctls_trackbar32,1342242840
Control5=IDC_LIGHT,msctls_trackbar32,1342242840
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_CHECK_COLORIZE,button,1342242819
Control10=IDC_HUEVAL,static,1342308354
Control11=IDC_SATVAL,static,1342308354
Control12=IDC_LIGHTVAL,static,1342308354

[CLS:CHueSatDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CHueSatDlg

[DLG:IDD_OIL]
Type=1
Class=COilDlg
ControlCount=10
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_BRUSH,msctls_trackbar32,1342242817
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342308352
Control6=IDC_SMOOTH,msctls_trackbar32,1342242840
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352

[CLS:COilDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=COilDlg

[DLG:IDD_RANDBLUR]
Type=1
Class=CRandBlurDlg
ControlCount=14
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_BLOCKSIZE,msctls_trackbar32,1342242832
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_BLURRAD,msctls_trackbar32,1342242832
Control8=IDC_DENSITY,msctls_trackbar32,1342242832
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352
Control12=IDC_STATIC,static,1342308352
Control13=IDC_STATIC,static,1342308352
Control14=IDC_STATIC,static,1342308352

[DLG:IDD_CONVOLUTION]
Type=1
Class=CConvolutionDlg
ControlCount=27
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_EDIT1,edit,1350631552
Control4=IDC_EDIT2,edit,1350631552
Control5=IDC_EDIT3,edit,1350631552
Control6=IDC_EDIT4,edit,1350631552
Control7=IDC_EDIT5,edit,1350631552
Control8=IDC_EDIT6,edit,1350631552
Control9=IDC_EDIT7,edit,1350631552
Control10=IDC_EDIT8,edit,1350631552
Control11=IDC_EDIT9,edit,1350631552
Control12=IDC_EDIT10,edit,1350631552
Control13=IDC_EDIT11,edit,1350631552
Control14=IDC_EDIT12,edit,1350631552
Control15=IDC_EDIT13,edit,1350631552
Control16=IDC_EDIT14,edit,1350631552
Control17=IDC_EDIT15,edit,1350631552
Control18=IDC_EDIT16,edit,1350631552
Control19=IDC_EDIT17,edit,1350631552
Control20=IDC_EDIT18,edit,1350631552
Control21=IDC_EDIT19,edit,1350631552
Control22=IDC_EDIT20,edit,1350631552
Control23=IDC_EDIT21,edit,1350631552
Control24=IDC_EDIT22,edit,1350631552
Control25=IDC_EDIT23,edit,1350631552
Control26=IDC_EDIT24,edit,1350631552
Control27=IDC_EDIT25,edit,1350631552

[CLS:CConvolutionDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CConvolutionDlg

[DLG:IDD_FROSTGLASS]
Type=1
Class=CFrostGlassDlg
ControlCount=6
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,static,1342308352
Control4=IDC_FROST,msctls_trackbar32,1342242817
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352

[CLS:CFrostGlassDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CFrostGlassDlg

[CLS:CRandBlurDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
LastObject=CRandBlurDlg

[DLG:IDD_MEDIANBLUR]
Type=1
Class=CMedianBlurDlg
ControlCount=4
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_RADIUS,edit,1350639744
Control4=IDC_STATIC,static,1342308352

[CLS:CMedianBlurDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CMedianBlurDlg

[DLG:IDD_RAINDROPS]
Type=1
Class=CRaindropsDlg
ControlCount=11
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_DROPSIZE,msctls_trackbar32,1342242840
Control4=IDC_DENSITY,msctls_trackbar32,1342242840
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_HIGHLIGHT,button,1342251011

[CLS:CRaindropsDlg]
Type=0
HeaderFile=dialogs.h
ImplementationFile=dialogs.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=CRaindropsDlg

[DLG:IDD_ABOUTBOX (English (U.S.))]
Type=1
Class=?
ControlCount=12
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352
Control12=IDC_STATIC,static,1342308352

[DLG:IDD_CONVOLUTION (English (U.S.))]
Type=1
Class=?
ControlCount=27
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_EDIT1,edit,1350631552
Control4=IDC_EDIT2,edit,1350631552
Control5=IDC_EDIT3,edit,1350631552
Control6=IDC_EDIT4,edit,1350631552
Control7=IDC_EDIT5,edit,1350631552
Control8=IDC_EDIT6,edit,1350631552
Control9=IDC_EDIT7,edit,1350631552
Control10=IDC_EDIT8,edit,1350631552
Control11=IDC_EDIT9,edit,1350631552
Control12=IDC_EDIT10,edit,1350631552
Control13=IDC_EDIT11,edit,1350631552
Control14=IDC_EDIT12,edit,1350631552
Control15=IDC_EDIT13,edit,1350631552
Control16=IDC_EDIT14,edit,1350631552
Control17=IDC_EDIT15,edit,1350631552
Control18=IDC_EDIT16,edit,1350631552
Control19=IDC_EDIT17,edit,1350631552
Control20=IDC_EDIT18,edit,1350631552
Control21=IDC_EDIT19,edit,1350631552
Control22=IDC_EDIT20,edit,1350631552
Control23=IDC_EDIT21,edit,1350631552
Control24=IDC_EDIT22,edit,1350631552
Control25=IDC_EDIT23,edit,1350631552
Control26=IDC_EDIT24,edit,1350631552
Control27=IDC_EDIT25,edit,1350631552

[DLG:IDD_EQINTENSITY (English (U.S.))]
Type=1
Class=?
ControlCount=4
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_INTENSITY,edit,1350631552
Control4=IDC_STATIC,static,1342308352

[DLG:IDD_HALFTONE (English (U.S.))]
Type=1
Class=?
ControlCount=5
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_HALFTONES,edit,1350631552
Control4=IDC_STATIC,static,1342308352
Control5=IDC_CHECK_COLOR,button,1342242819

[DLG:IDD_OIL (English (U.S.))]
Type=1
Class=?
ControlCount=10
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_BRUSH,msctls_trackbar32,1342242817
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342308352
Control6=IDC_SMOOTH,msctls_trackbar32,1342242840
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352

[DLG:IDD_FROSTGLASS (English (U.S.))]
Type=1
Class=?
ControlCount=6
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,static,1342308352
Control4=IDC_FROST,msctls_trackbar32,1342242817
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352

[DLG:IDD_MEDIANBLUR (English (U.S.))]
Type=1
Class=?
ControlCount=4
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_RADIUS,edit,1350639744
Control4=IDC_STATIC,static,1342308352

[DLG:IDD_HUESAT (English (U.S.))]
Type=1
Class=?
ControlCount=12
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_HUE,msctls_trackbar32,1342242840
Control4=IDC_SAT,msctls_trackbar32,1342242840
Control5=IDC_LIGHT,msctls_trackbar32,1342242840
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_CHECK_COLORIZE,button,1342242819
Control10=IDC_HUEVAL,static,1342308354
Control11=IDC_SATVAL,static,1342308354
Control12=IDC_LIGHTVAL,static,1342308354

[DLG:IDD_FISHEYE (English (U.S.))]
Type=1
Class=?
ControlCount=11
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,button,1342177287
Control4=IDC_STATIC,button,1342177287
Control5=IDC_RADIO_ALONE,button,1342308361
Control6=IDC_RADIO_WHITE,button,1342177289
Control7=IDC_RADIO_BLACK,button,1342177289
Control8=IDC_CHECK_INVERSE,button,1342242819
Control9=IDC_SLIDER_CURVE,msctls_trackbar32,1342242840
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352

[DLG:IDD_RANDBLUR (English (U.S.))]
Type=1
Class=?
ControlCount=14
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_BLOCKSIZE,msctls_trackbar32,1342242832
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_BLURRAD,msctls_trackbar32,1342242832
Control8=IDC_DENSITY,msctls_trackbar32,1342242832
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_STATIC,static,1342308352
Control12=IDC_STATIC,static,1342308352
Control13=IDC_STATIC,static,1342308352
Control14=IDC_STATIC,static,1342308352

[DLG:IDD_RAINDROPS (English (U.S.))]
Type=1
Class=?
ControlCount=11
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_DROPSIZE,msctls_trackbar32,1342242840
Control4=IDC_DENSITY,msctls_trackbar32,1342242840
Control5=IDC_STATIC,static,1342308352
Control6=IDC_STATIC,static,1342308352
Control7=IDC_STATIC,static,1342308352
Control8=IDC_STATIC,static,1342308352
Control9=IDC_STATIC,static,1342308352
Control10=IDC_STATIC,static,1342308352
Control11=IDC_HIGHLIGHT,button,1342251011

[DLG:IDD_BLUR (English (U.S.))]
Type=1
Class=?
ControlCount=5
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_RADIUS,edit,1350639744
Control4=IDC_STATIC,static,1342308352
Control5=IDC_CHECKFAST,button,1342242819

[MNU:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_OPEN
Command2=ID_FILE_SAVE
Command3=ID_FILE_SAVE_AS
Command4=ID_APP_EXIT
Command5=ID_UNDO
Command6=ID_FILTER_GRAYSCALE_BYINTENSITY
Command7=ID_FILTER_GRAYSCALE_BYHUE
Command8=ID_FILTER_GRAYSCALE_BYSATURATION
Command9=ID_FILTER_HUESATURATION
Command10=ID_FILTER_EQUALIZEINTENSITY
Command11=ID_FILTER_BLUR
Command12=ID_FILTER_MEDIANBLUR
Command13=ID_FILTER_CONVOLUTION
Command14=ID_FILTER_FISHEYELENS
Command15=ID_FILTER_POLARCOORDINATES
Command16=ID_FILTER_OILPAINT
Command17=ID_FILTER_FROSTEDGLASS
Command18=ID_FILTER_RANDOMBLUR
Command19=ID_FILTER_RAINDROPS
Command20=ID_VIEW_TOOLBAR
Command21=ID_VIEW_STATUS_BAR
Command22=ID_APP_ABOUT
CommandCount=22

[TB:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_OPEN
Command2=ID_FILE_SAVE_AS
Command3=ID_UNDO
Command4=ID_APP_ABOUT
CommandCount=4

[ACL:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_UNDO
Command5=ID_EDIT_CUT
Command6=ID_EDIT_COPY
Command7=ID_EDIT_PASTE
Command8=ID_EDIT_UNDO
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_NEXT_PANE
Command13=ID_PREV_PANE
CommandCount=13

[DLG:CG_IDD_PROGRESS (English (U.S.))]
Type=1
Class=?
ControlCount=3
Control1=IDCANCEL,button,1342242817
Control2=CG_IDC_PROGDLG_PROGRESS,msctls_progress32,1350565888
Control3=CG_IDC_PROGDLG_PERCENT,static,1342308352

