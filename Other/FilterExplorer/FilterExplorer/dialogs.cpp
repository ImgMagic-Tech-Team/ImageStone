//  Dialogs.cpp : implementation file
// CG: This file was added by the Progress Dialog component

#include "stdafx.h"
#include "resource.h"
#include "Dialogs.h"

#ifdef _DEBUG
#undef THIS_FILE
static char BASED_CODE THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CProgressDlg dialog

CProgressDlg::CProgressDlg(UINT nCaptionID)
{
	m_nCaptionID = CG_IDS_PROGRESS_CAPTION;
	if (nCaptionID != 0)
		m_nCaptionID = nCaptionID;

    m_bCancel=FALSE;
    m_nLower=0;
    m_nUpper=100;
    m_nStep=1;
    //{{AFX_DATA_INIT(CProgressDlg)
    // NOTE: the ClassWizard will add member initialization here
    //}}AFX_DATA_INIT
    m_bParentDisabled = FALSE;
}

CProgressDlg::~CProgressDlg()
{
    if(m_hWnd!=NULL)
      DestroyWindow();
}

BOOL CProgressDlg::DestroyWindow()
{
    ReEnableParent();
    return CDialog::DestroyWindow();
}

void CProgressDlg::ReEnableParent()
{
    if(m_bParentDisabled && (m_pParentWnd!=NULL))
      m_pParentWnd->EnableWindow(TRUE);
    m_bParentDisabled=FALSE;
}

BOOL CProgressDlg::Create(CWnd *pParent)
{
    // Get the true parent of the dialog
    m_pParentWnd = CWnd::GetSafeOwner(pParent);

    // m_bParentDisabled is used to re-enable the parent window
    // when the dialog is destroyed. So we don't want to set
    // it to TRUE unless the parent was already enabled.

    if((m_pParentWnd!=NULL) && m_pParentWnd->IsWindowEnabled())
    {
      m_pParentWnd->EnableWindow(FALSE);
      m_bParentDisabled = TRUE;
    }

    if(!CDialog::Create(CProgressDlg::IDD,pParent))
    {
      ReEnableParent();
      return FALSE;
    }

    return TRUE;
}

void CProgressDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialog::DoDataExchange(pDX);
    //{{AFX_DATA_MAP(CProgressDlg)
    DDX_Control(pDX, CG_IDC_PROGDLG_PROGRESS, m_Progress);
    //}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CProgressDlg, CDialog)
    //{{AFX_MSG_MAP(CProgressDlg)
    //}}AFX_MSG_MAP
END_MESSAGE_MAP()


void CProgressDlg::OnCancel()
{
    m_bCancel=TRUE;
}

void CProgressDlg::SetRange(int nLower,int nUpper)
{
    m_nLower = nLower;
    m_nUpper = nUpper;
    m_Progress.SetRange(nLower,nUpper);
}
  
int CProgressDlg::SetPos(int nPos)
{
    PumpMessages();
    int iResult = m_Progress.SetPos(nPos);
    UpdatePercent(nPos);
    return iResult;
}

int CProgressDlg::SetStep(int nStep)
{
    m_nStep = nStep; // Store for later use in calculating percentage
    return m_Progress.SetStep(nStep);
}

int CProgressDlg::OffsetPos(int nPos)
{
    PumpMessages();
    int iResult = m_Progress.OffsetPos(nPos);
    UpdatePercent(iResult+nPos);
    return iResult;
}

int CProgressDlg::StepIt()
{
    PumpMessages();
    int iResult = m_Progress.StepIt();
    UpdatePercent(iResult+m_nStep);
    return iResult;
}

void CProgressDlg::PumpMessages()
{
    // Must call Create() before using the dialog
    ASSERT(m_hWnd!=NULL);

    MSG msg;
    // Handle dialog messages
    while(PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
    {
      if(!IsDialogMessage(&msg))
      {
        TranslateMessage(&msg);
        DispatchMessage(&msg);  
      }
    }
}

BOOL CProgressDlg::CheckCancelButton()
{
    // Process all pending messages
    PumpMessages();

    // Reset m_bCancel to FALSE so that
    // CheckCancelButton returns FALSE until the user
    // clicks Cancel again. This will allow you to call
    // CheckCancelButton and still continue the operation.
    // If m_bCancel stayed TRUE, then the next call to
    // CheckCancelButton would always return TRUE

    BOOL bResult = m_bCancel;
    m_bCancel = FALSE;

    return bResult;
}

void CProgressDlg::UpdatePercent(int nNewPos)
{
    CWnd *pWndPercent = GetDlgItem(CG_IDC_PROGDLG_PERCENT);
    int nPercent;
    
    int nDivisor = m_nUpper - m_nLower;
    ASSERT(nDivisor>0);  // m_nLower should be smaller than m_nUpper

    int nDividend = (nNewPos - m_nLower);
    ASSERT(nDividend>=0);   // Current position should be greater than m_nLower

    nPercent = nDividend * 100 / nDivisor;

    // Since the Progress Control wraps, we will wrap the percentage
    // along with it. However, don't reset 100% back to 0%
    if(nPercent!=100)
      nPercent %= 100;

    // Display the percentage
    CString strBuf;
    strBuf.Format(_T("%d%c"),nPercent,_T('%'));

	CString strCur; // get current percentage
    pWndPercent->GetWindowText(strCur);

	if (strCur != strBuf)
		pWndPercent->SetWindowText(strBuf);
}
    
/////////////////////////////////////////////////////////////////////////////
// CProgressDlg message handlers

BOOL CProgressDlg::OnInitDialog() 
{
    CDialog::OnInitDialog();
    m_Progress.SetRange(m_nLower,m_nUpper);
    m_Progress.SetStep(m_nStep);
    m_Progress.SetPos(m_nLower);

	CString strCaption;
	VERIFY(strCaption.LoadString(m_nCaptionID));
    SetWindowText(strCaption);

    return TRUE;  
}


/////////////////////////////////////////////////////////////////////////////
// CBlurDlg dialog


CBlurDlg::CBlurDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBlurDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CBlurDlg)
	m_nRadius = 5;
	m_bFast = FALSE;
	//}}AFX_DATA_INIT
}


void CBlurDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CBlurDlg)
	DDX_Text(pDX, IDC_RADIUS, m_nRadius);
	DDV_MinMaxInt(pDX, m_nRadius, 1, 25);
	DDX_Check(pDX, IDC_CHECKFAST, m_bFast);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CBlurDlg, CDialog)
	//{{AFX_MSG_MAP(CBlurDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CBlurDlg message handlers
/////////////////////////////////////////////////////////////////////////////
// CEqIntensityDlg dialog


CEqIntensityDlg::CEqIntensityDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CEqIntensityDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CEqIntensityDlg)
	m_iIntensity = 0;
	//}}AFX_DATA_INIT
}


void CEqIntensityDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CEqIntensityDlg)
	DDX_Text(pDX, IDC_INTENSITY, m_iIntensity);
	DDV_MinMaxInt(pDX, m_iIntensity, 0, 100);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CEqIntensityDlg, CDialog)
	//{{AFX_MSG_MAP(CEqIntensityDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CEqIntensityDlg message handlers
/////////////////////////////////////////////////////////////////////////////
// CFishEyeDlg dialog


CFishEyeDlg::CFishEyeDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFishEyeDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFishEyeDlg)
	m_bInverse = FALSE;
	m_iBackground = 0;
	m_iCurvature = 0;
	//}}AFX_DATA_INIT
}


void CFishEyeDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFishEyeDlg)
	DDX_Check(pDX, IDC_CHECK_INVERSE, m_bInverse);
	DDX_Radio(pDX, IDC_RADIO_ALONE, m_iBackground);
	DDX_Slider(pDX, IDC_SLIDER_CURVE, m_iCurvature);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CFishEyeDlg, CDialog)
	//{{AFX_MSG_MAP(CFishEyeDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFishEyeDlg message handlers

BOOL CFishEyeDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	CSliderCtrl* pSlide = (CSliderCtrl*) GetDlgItem(IDC_SLIDER_CURVE);
	pSlide->SetRange(1,100);
	pSlide->SetPos(25);
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
/////////////////////////////////////////////////////////////////////////////
// CHalftoneDlg dialog


CHalftoneDlg::CHalftoneDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CHalftoneDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CHalftoneDlg)
	m_bColor = FALSE;
	m_iHalftones = 0;
	//}}AFX_DATA_INIT
}


void CHalftoneDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CHalftoneDlg)
	DDX_Check(pDX, IDC_CHECK_COLOR, m_bColor);
	DDX_Text(pDX, IDC_HALFTONES, m_iHalftones);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CHalftoneDlg, CDialog)
	//{{AFX_MSG_MAP(CHalftoneDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CHalftoneDlg message handlers
/////////////////////////////////////////////////////////////////////////////
// CHueSatDlg dialog


CHueSatDlg::CHueSatDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CHueSatDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CHueSatDlg)
	m_bColorize = FALSE;
	m_iHue = 0;
	m_iLight = 0;
	m_iSat = 0;
	//}}AFX_DATA_INIT
}


void CHueSatDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CHueSatDlg)
	DDX_Check(pDX, IDC_CHECK_COLORIZE, m_bColorize);
	DDX_Slider(pDX, IDC_HUE, m_iHue);
	DDX_Slider(pDX, IDC_LIGHT, m_iLight);
	DDX_Slider(pDX, IDC_SAT, m_iSat);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CHueSatDlg, CDialog)
	//{{AFX_MSG_MAP(CHueSatDlg)
	ON_WM_HSCROLL()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CHueSatDlg message handlers

BOOL CHueSatDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CSliderCtrl* pHue   = (CSliderCtrl*) GetDlgItem(IDC_HUE);
	CSliderCtrl* pSat   = (CSliderCtrl*) GetDlgItem(IDC_SAT);
	CSliderCtrl* pLight = (CSliderCtrl*) GetDlgItem(IDC_LIGHT);

	pHue->SetRange(-180,180,TRUE);
	pHue->SetPos(0);

	pSat->SetRange(-100,100,TRUE);
	pSat->SetPos(0);

	pLight->SetRange(-100,100,TRUE);
	pLight->SetPos(0);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CHueSatDlg::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
	CSliderCtrl* pSlide = (CSliderCtrl*) pScrollBar;
	CString strText;

	switch(pScrollBar->GetDlgCtrlID())
	{
		case IDC_HUE:	strText.Format("%d", pSlide->GetPos());
						SetDlgItemText(IDC_HUEVAL, strText);
						break;
		case IDC_SAT:	strText.Format("%d", pSlide->GetPos());
						SetDlgItemText(IDC_SATVAL, strText);
						break;
		case IDC_LIGHT: strText.Format("%d", pSlide->GetPos());
						SetDlgItemText(IDC_LIGHTVAL, strText);
						break;
	}
	
	CDialog::OnHScroll(nSBCode, nPos, pScrollBar);
}
/////////////////////////////////////////////////////////////////////////////
// COilDlg dialog


COilDlg::COilDlg(CWnd* pParent /*=NULL*/)
	: CDialog(COilDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(COilDlg)
	m_iBrush = 0;
	m_iSmooth = 0;
	//}}AFX_DATA_INIT
}


void COilDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(COilDlg)
	DDX_Slider(pDX, IDC_BRUSH, m_iBrush);
	DDX_Slider(pDX, IDC_SMOOTH, m_iSmooth);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(COilDlg, CDialog)
	//{{AFX_MSG_MAP(COilDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// COilDlg message handlers

BOOL COilDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CSliderCtrl* pBrushSlider = (CSliderCtrl*) GetDlgItem(IDC_BRUSH);
	pBrushSlider->SetRange(1,5);
	pBrushSlider->SetPos(3);

	CSliderCtrl* pSmoothSlider = (CSliderCtrl*) GetDlgItem(IDC_SMOOTH);
	pSmoothSlider->SetRange(10,255);
	pSmoothSlider->SetPos(100);
	
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
/////////////////////////////////////////////////////////////////////////////
// CRandBlurDlg dialog


CRandBlurDlg::CRandBlurDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CRandBlurDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CRandBlurDlg)
	m_iSize = 0;
	m_iRadius = 0;
	m_iAmount = 0;
	//}}AFX_DATA_INIT
}


void CRandBlurDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CRandBlurDlg)
	DDX_Slider(pDX, IDC_BLOCKSIZE, m_iSize);
	DDX_Slider(pDX, IDC_BLURRAD, m_iRadius);
	DDX_Slider(pDX, IDC_DENSITY, m_iAmount);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CRandBlurDlg, CDialog)
	//{{AFX_MSG_MAP(CRandBlurDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CRandBlurDlg message handlers

BOOL CRandBlurDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CSliderCtrl* pSize = (CSliderCtrl*) GetDlgItem(IDC_BLOCKSIZE);
	CSliderCtrl* pRad  = (CSliderCtrl*) GetDlgItem(IDC_BLURRAD);
	CSliderCtrl* pDen  = (CSliderCtrl*) GetDlgItem(IDC_DENSITY);

	pSize->SetRange(25,100,TRUE);
	pSize->SetPos(40);

	pRad->SetRange(1,24,TRUE);
	pRad->SetPos(12);

	pDen->SetRange(1,500,TRUE);
	pDen->SetPos(375);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
/////////////////////////////////////////////////////////////////////////////
// CConvolutionDlg dialog


CConvolutionDlg::CConvolutionDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CConvolutionDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CConvolutionDlg)
	m_iPos11 = 0;
	m_iPos25 = 0;
	m_iPos31 = 0;
	m_iPos32 = 0;
	m_iPos33 = 0;
	m_iPos34 = 0;
	m_iPos35 = 0;
	m_iPos41 = 0;
	m_iPos42 = 0;
	m_iPos43 = 0;
	m_iPos44 = 0;
	m_iPos12 = 0;
	m_iPos45 = 0;
	m_iPos51 = 0;
	m_iPos52 = 0;
	m_iPos53 = 0;
	m_iPos54 = 0;
	m_iPos55 = 0;
	m_iPos13 = 0;
	m_iPos14 = 0;
	m_iPos15 = 0;
	m_iPos21 = 0;
	m_iPos22 = 0;
	m_iPos23 = 0;
	m_iPos24 = 0;
	//}}AFX_DATA_INIT
}


void CConvolutionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CConvolutionDlg)
	DDX_Text(pDX, IDC_EDIT1, m_iPos11);
	DDX_Text(pDX, IDC_EDIT10, m_iPos25);
	DDX_Text(pDX, IDC_EDIT11, m_iPos31);
	DDX_Text(pDX, IDC_EDIT12, m_iPos32);
	DDX_Text(pDX, IDC_EDIT13, m_iPos33);
	DDX_Text(pDX, IDC_EDIT14, m_iPos34);
	DDX_Text(pDX, IDC_EDIT15, m_iPos35);
	DDX_Text(pDX, IDC_EDIT16, m_iPos41);
	DDX_Text(pDX, IDC_EDIT17, m_iPos42);
	DDX_Text(pDX, IDC_EDIT18, m_iPos43);
	DDX_Text(pDX, IDC_EDIT19, m_iPos44);
	DDX_Text(pDX, IDC_EDIT2, m_iPos12);
	DDX_Text(pDX, IDC_EDIT20, m_iPos45);
	DDX_Text(pDX, IDC_EDIT21, m_iPos51);
	DDX_Text(pDX, IDC_EDIT22, m_iPos52);
	DDX_Text(pDX, IDC_EDIT23, m_iPos53);
	DDX_Text(pDX, IDC_EDIT24, m_iPos54);
	DDX_Text(pDX, IDC_EDIT25, m_iPos55);
	DDX_Text(pDX, IDC_EDIT3, m_iPos13);
	DDX_Text(pDX, IDC_EDIT4, m_iPos14);
	DDX_Text(pDX, IDC_EDIT5, m_iPos15);
	DDX_Text(pDX, IDC_EDIT6, m_iPos21);
	DDX_Text(pDX, IDC_EDIT7, m_iPos22);
	DDX_Text(pDX, IDC_EDIT8, m_iPos23);
	DDX_Text(pDX, IDC_EDIT9, m_iPos24);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CConvolutionDlg, CDialog)
	//{{AFX_MSG_MAP(CConvolutionDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CConvolutionDlg message handlers
/////////////////////////////////////////////////////////////////////////////
// CFrostGlassDlg dialog


CFrostGlassDlg::CFrostGlassDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFrostGlassDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFrostGlassDlg)
	m_iFrost = 0;
	//}}AFX_DATA_INIT
}


void CFrostGlassDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFrostGlassDlg)
	DDX_Slider(pDX, IDC_FROST, m_iFrost);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CFrostGlassDlg, CDialog)
	//{{AFX_MSG_MAP(CFrostGlassDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFrostGlassDlg message handlers

BOOL CFrostGlassDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CSliderCtrl* pFrostSlider = (CSliderCtrl*) GetDlgItem(IDC_FROST);
	pFrostSlider->SetRange(1,10);
	pFrostSlider->SetPos(4);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
/////////////////////////////////////////////////////////////////////////////
// CMedianBlurDlg dialog


CMedianBlurDlg::CMedianBlurDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMedianBlurDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMedianBlurDlg)
	m_iRadius = 2;
	//}}AFX_DATA_INIT
}


void CMedianBlurDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMedianBlurDlg)
	DDX_Text(pDX, IDC_RADIUS, m_iRadius);
	DDV_MinMaxInt(pDX, m_iRadius, 1, 10);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CMedianBlurDlg, CDialog)
	//{{AFX_MSG_MAP(CMedianBlurDlg)
		// NOTE: the ClassWizard will add message map macros here
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMedianBlurDlg message handlers
/////////////////////////////////////////////////////////////////////////////
// CRaindropsDlg dialog


CRaindropsDlg::CRaindropsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CRaindropsDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CRaindropsDlg)
	m_iAmount = 0;
	m_iSize = 0;
	m_bHighlight = FALSE;
	//}}AFX_DATA_INIT
}


void CRaindropsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CRaindropsDlg)
	DDX_Slider(pDX, IDC_DENSITY, m_iAmount);
	DDX_Slider(pDX, IDC_DROPSIZE, m_iSize);
	DDX_Check(pDX, IDC_HIGHLIGHT, m_bHighlight);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CRaindropsDlg, CDialog)
	//{{AFX_MSG_MAP(CRaindropsDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CRaindropsDlg message handlers

BOOL CRaindropsDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CSliderCtrl* pSize =  (CSliderCtrl*) GetDlgItem(IDC_DROPSIZE);
	CSliderCtrl* pDen  =  (CSliderCtrl*) GetDlgItem(IDC_DENSITY);
	CButton*     pCheck = (CButton*)     GetDlgItem(IDC_HIGHLIGHT);

	pSize->SetRange(20,100,TRUE);
	pSize->SetPos(40);

	pDen->SetRange(1,1000,TRUE);
	pDen->SetPos(250);

	pCheck->SetCheck(1);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
