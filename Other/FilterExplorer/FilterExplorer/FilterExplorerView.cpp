/////////////////////////////////////////////////////////////////////////////
//
// FilterExplorer
// Wittenberg University, Springfield, Ohio
// Computer Science Honors Thesis
// 18 April 2001
//
// Filename.....: FilterExplorerView.cpp
// Compilation..: Microsoft Visual C++ 6.0.
// Description..: implementation for CFilterExplorerView class
//
// Written by...: Jason Waltman
//
/////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "FilterExplorer.h"

#include "FilterExplorerDoc.h"
#include "FilterExplorerView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerView

IMPLEMENT_DYNCREATE(CFilterExplorerView, CScrollView)

BEGIN_MESSAGE_MAP(CFilterExplorerView, CScrollView)
	//{{AFX_MSG_MAP(CFilterExplorerView)
	ON_COMMAND(ID_FILE_OPEN, OnFileOpen)
	ON_COMMAND(ID_FILE_SAVE, OnFileSave)
	ON_COMMAND(ID_FILE_SAVE_AS, OnFileSaveAs)
	ON_COMMAND(ID_UNDO, OnUndo)
	ON_COMMAND(ID_FILTER_GRAYSCALE_BYINTENSITY, OnFilterGrayscaleByIntensity)
	ON_COMMAND(ID_FILTER_GRAYSCALE_BYHUE, OnFilterGrayscaleByHue)
	ON_COMMAND(ID_FILTER_GRAYSCALE_BYSATURATION, OnFilterGrayscaleBySaturation)
	ON_COMMAND(ID_FILTER_HUESATURATION, OnFilterHueSaturation)
	ON_COMMAND(ID_FILTER_EQUALIZEINTENSITY, OnFilterEqualizeIntensity)
	ON_COMMAND(ID_FILTER_BLUR, OnFilterBlur)
	ON_COMMAND(ID_FILTER_MEDIANBLUR, OnFilterMedianBlur)
	ON_COMMAND(ID_FILTER_CONVOLUTION, OnFilterConvolution)
	ON_COMMAND(ID_FILTER_OILPAINT, OnFilterOilPaint)
	ON_COMMAND(ID_FILTER_FROSTEDGLASS, OnFilterFrostedglass)
	ON_COMMAND(ID_FILTER_FISHEYELENS, OnFilterFishEyeLens)
	ON_COMMAND(ID_FILTER_POLARCOORDINATES, OnFilterPolarCoordinates)
	ON_COMMAND(ID_FILTER_RANDOMBLUR, OnFilterRandomBlur)
	ON_COMMAND(ID_FILTER_RAINDROPS, OnFilterRaindrops)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerView construction/destruction

CFilterExplorerView::CFilterExplorerView()
{
	// TODO: add construction code here

}

CFilterExplorerView::~CFilterExplorerView()
{
}

BOOL CFilterExplorerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CScrollView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerView drawing

void CFilterExplorerView::OnDraw(CDC* pDC)
{
	CFilterExplorerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);

	// draw the current image
	CRect rectClient;
	GetClientRect(rectClient);
	currentimage.DrawBMP(pDC, rectClient);
	ReleaseDC(pDC);	
}

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerView diagnostics

#ifdef _DEBUG
void CFilterExplorerView::AssertValid() const
{
	CScrollView::AssertValid();
}

void CFilterExplorerView::Dump(CDumpContext& dc) const
{
	CScrollView::Dump(dc);
}

CFilterExplorerDoc* CFilterExplorerView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CFilterExplorerDoc)));
	return (CFilterExplorerDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerView message handlers

void CFilterExplorerView::OnInitialUpdate() 
{
	CScrollView::OnInitialUpdate();
	
	// set up bare scroll bar info to make compiler happy
	// the actual info is set when a file is opened

	CSize sizeTotal(0,0);
	CSize sizePage(0,0);
	CSize sizeLine(0,0);
	SetScrollSizes(MM_TEXT, sizeTotal, sizePage, sizeLine);	
}

void CFilterExplorerView::OnFileOpen() 
{
	CString sFilename;
	CString sFilt="JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|All files (*.*)|*.*||";

	CFileDialog dlg(
		TRUE,				// Create an open file dialog
		"*.JPG",			// Default file extension
		NULL,				// Filename
		OFN_HIDEREADONLY |	// Flags - don't show the read only check box)
		OFN_FILEMUSTEXIST,	//       - make sure file exists
		sFilt);				// the file filter

	if (dlg.DoModal()==IDOK)
	{
		CWaitCursor wait;	// change cursor to wait

		sFilename = dlg.GetPathName();
		CString sExt = dlg.GetFileExt();
		CString sFilenameShort= dlg.GetFileName();

		CFilterExplorerDoc* pDoc = GetDocument();

		if (!sExt.CompareNoCase("JPG"))		// returns 0 if equal
		{
			currentimage.SetFilename(sFilename);
			currentimage.LoadJPG();
			pDoc->SetTitle(sFilenameShort);
		}
		else if (!sExt.CompareNoCase("BMP"))
		{
			currentimage.SetFilename(sFilename);
			currentimage.LoadBMP();
			pDoc->SetTitle(sFilenameShort);
		}
		else
			AfxMessageBox("Incorrect File Type.", MB_ICONSTOP);
		
		wait.Restore();		// put cursor back to normal
	}

	// set logical size to size of image; set up scroll bars for this size

	CSize sizeTotal(currentimage.GetWidth(), currentimage.GetHeight());
	CSize sizePage(sizeTotal.cx / 2, sizeTotal.cy / 2);
	CSize sizeLine(sizeTotal.cx / 50, sizeTotal.cy / 50);
	SetScrollSizes(MM_TEXT, sizeTotal, sizePage, sizeLine);

	Invalidate(TRUE);		// Refresh the entire view	
}

void CFilterExplorerView::OnFileSave() 
{
	if (currentimage.OK())
	{
		if(AfxMessageBox("This will overwrite your existing file.", MB_ICONEXCLAMATION | MB_OKCANCEL)==IDOK)
		{
			CWaitCursor wait;					// change cursor to wait

			CString sFilename = currentimage.GetFilename();
			CString sExt = sFilename.Right(3);

			if (!sExt.CompareNoCase("JPG"))		// returns 0 if equal
				currentimage.SaveJPG(sFilename);

			if (!sExt.CompareNoCase("BMP"))
				currentimage.SaveBMP(sFilename);

			// disable undo after save	
			CWnd* pMain = AfxGetMainWnd();
			if (pMain != NULL)
			{
				CMenu* pMenu = pMain->GetMenu();
				pMenu->EnableMenuItem(ID_UNDO, MF_DISABLED | MF_GRAYED);
			}

			Invalidate(TRUE);					// Refresh the entire view	
			
			wait.Restore();						// put cursor back to normal
		}
	}
}

void CFilterExplorerView::OnFileSaveAs()
{
	if (currentimage.OK())
	{
		CString sFilename = currentimage.GetFilename();
		CString sFilt="JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp|";

		CFileDialog dlg(
			FALSE,							// Create a save as file dialog
			"*.JPG",						// Default file extension
			sFilename,						// Filename
			OFN_HIDEREADONLY| 				// Flags - don't show the read only check box
			OFN_FILEMUSTEXIST|				//       - make sure file exists
			OFN_OVERWRITEPROMPT,			//       - ask about overwrite if file exists
			sFilt);							// the file filter

		if (dlg.DoModal()==IDOK)
		{
			CWaitCursor wait;				// change cursor to wait

			sFilename = dlg.GetPathName();
			CString sExt = dlg.GetFileExt();
			CString sFilenameShort= dlg.GetFileName();

			CFilterExplorerDoc* pDoc = GetDocument();

			if (!sExt.CompareNoCase("JPG"))		// returns 0 if equal
			{
				currentimage.SetFilename(sFilename);
				currentimage.SaveJPG(sFilename);
				pDoc->SetTitle(sFilenameShort);
			}

			if (!sExt.CompareNoCase("BMP"))
			{
				currentimage.SetFilename(sFilename);
				currentimage.SaveBMP(sFilename);
				pDoc->SetTitle(sFilenameShort);
			}

			// disable undo after save	
			CWnd* pMain = AfxGetMainWnd();
			if (pMain != NULL)
			{
				CMenu* pMenu = pMain->GetMenu();
				pMenu->EnableMenuItem(ID_UNDO, MF_DISABLED | MF_GRAYED);
			}

			Invalidate(TRUE);				// Refresh the entire view	

			wait.Restore();					// put cursor back to normal
		}
	}
}

void CFilterExplorerView::OnUndo() 
{
	CWnd* pMain = AfxGetMainWnd();
	if (pMain != NULL)
	{
		CMenu* pMenu = pMain->GetMenu();
		UINT state = pMenu->GetMenuState(ID_UNDO, MF_BYCOMMAND);
		ASSERT(state != 0xFFFFFFFF);
		
		if (state & MF_GRAYED)
		{
			// do nothing if undo is grayed
		}
		else
		{
			currentimage.ProcessFilter(UNDO);
			pMenu->EnableMenuItem(ID_UNDO, MF_DISABLED | MF_GRAYED); //disable undo
			CWaitCursor wait;
			Invalidate(TRUE);
			wait.Restore();
		}
	}
}

void CFilterExplorerView::OnFilterGrayscaleByIntensity() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(GRAYINT);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}
}

void CFilterExplorerView::OnFilterGrayscaleByHue() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(GRAYHUE);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterGrayscaleBySaturation() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(GRAYSAT);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterHueSaturation() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(HUESAT);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterEqualizeIntensity() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(EQINTENSITY);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterBlur() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(BLUR);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterMedianBlur() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(MEDIANBLUR);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterConvolution() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(CONVOLUTION);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}		
}

void CFilterExplorerView::OnFilterOilPaint() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(OIL);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}		
}

void CFilterExplorerView::OnFilterFrostedglass() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(FROSTGLASS);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}		
}

void CFilterExplorerView::OnFilterFishEyeLens() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(FISHEYE);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}		
}

void CFilterExplorerView::OnFilterPolarCoordinates() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(POLAR);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}		
}

void CFilterExplorerView::OnFilterRandomBlur() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(RANDBLUR);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}

void CFilterExplorerView::OnFilterRaindrops() 
{
	if (currentimage.OK())
	{
		currentimage.ProcessFilter(RAINDROPS);
		CWaitCursor wait;
		Invalidate(TRUE);
		wait.Restore();
	}	
}
