// FilterExplorerDoc.h : interface of the CFilterExplorerDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILTEREXPLORERDOC_H__0EA9B31E_7E51_4051_8EA5_600FF4C0B002__INCLUDED_)
#define AFX_FILTEREXPLORERDOC_H__0EA9B31E_7E51_4051_8EA5_600FF4C0B002__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CFilterExplorerDoc : public CDocument
{
protected: // create from serialization only
	CFilterExplorerDoc();
	DECLARE_DYNCREATE(CFilterExplorerDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFilterExplorerDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CFilterExplorerDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CFilterExplorerDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FILTEREXPLORERDOC_H__0EA9B31E_7E51_4051_8EA5_600FF4C0B002__INCLUDED_)
