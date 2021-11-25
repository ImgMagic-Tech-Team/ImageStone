// FilterExplorerView.h : interface of the CFilterExplorerView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILTEREXPLORERVIEW_H__D0AF98F6_FF1B_4689_BF41_C9603601A653__INCLUDED_)
#define AFX_FILTEREXPLORERVIEW_H__D0AF98F6_FF1B_4689_BF41_C9603601A653__INCLUDED_

#include "Filters.h"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CFilterExplorerView : public CScrollView
{
protected: // create from serialization only
	CFilterExplorerView();
	DECLARE_DYNCREATE(CFilterExplorerView)

// Attributes
public:
	CFilterExplorerDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFilterExplorerView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual void OnInitialUpdate();
	//}}AFX_VIRTUAL

// Implementation
public:
	CImage currentimage;				// current image displayed

	virtual ~CFilterExplorerView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CFilterExplorerView)
	afx_msg void OnFileOpen();
	afx_msg void OnFileSave();
	afx_msg void OnFileSaveAs();
	afx_msg void OnUndo();
	afx_msg void OnFilterGrayscaleByIntensity();
	afx_msg void OnFilterGrayscaleByHue();
	afx_msg void OnFilterGrayscaleBySaturation();
	afx_msg void OnFilterHueSaturation();
	afx_msg void OnFilterEqualizeIntensity();
	afx_msg void OnFilterBlur();
	afx_msg void OnFilterMedianBlur();
	afx_msg void OnFilterConvolution();
	afx_msg void OnFilterOilPaint();
	afx_msg void OnFilterFrostedglass();
	afx_msg void OnFilterFishEyeLens();
	afx_msg void OnFilterPolarCoordinates();
	afx_msg void OnFilterRandomBlur();
	afx_msg void OnFilterRaindrops();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in FilterExplorerView.cpp
inline CFilterExplorerDoc* CFilterExplorerView::GetDocument()
   { return (CFilterExplorerDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FILTEREXPLORERVIEW_H__D0AF98F6_FF1B_4689_BF41_C9603601A653__INCLUDED_)
