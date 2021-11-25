// FilterExplorerDoc.cpp : implementation of the CFilterExplorerDoc class
//

#include "stdafx.h"
#include "FilterExplorer.h"

#include "FilterExplorerDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerDoc

IMPLEMENT_DYNCREATE(CFilterExplorerDoc, CDocument)

BEGIN_MESSAGE_MAP(CFilterExplorerDoc, CDocument)
	//{{AFX_MSG_MAP(CFilterExplorerDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerDoc construction/destruction

CFilterExplorerDoc::CFilterExplorerDoc()
{
	// TODO: add one-time construction code here

}

CFilterExplorerDoc::~CFilterExplorerDoc()
{
}

BOOL CFilterExplorerDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerDoc serialization

void CFilterExplorerDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerDoc diagnostics

#ifdef _DEBUG
void CFilterExplorerDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CFilterExplorerDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CFilterExplorerDoc commands
