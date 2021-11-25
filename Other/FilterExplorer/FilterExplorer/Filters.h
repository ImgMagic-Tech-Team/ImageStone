/////////////////////////////////////////////////////////////////////////////
//
// FilterExplorer
// Wittenberg University, Springfield, Ohio
// Computer Science Honors Thesis
// 18 April 2001
//
// Filename.....: Filters.h
// Compilation..: Microsoft Visual C++ 6.0.
// Description..: interface for pixel, hsvpixel, CFilter, CImage classes
//
// Written by...: Jason Waltman
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILTER_H__D53069AA_EA08_4687_9202_7CAF73FE0011__INCLUDED_)
#define AFX_FILTER_H__D53069AA_EA08_4687_9202_7CAF73FE0011__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//////////////////////////////////////////////////////////////////////
// Pixel Classes 
//////////////////////////////////////////////////////////////////////

class hsvpixel;	 // forward reference

class pixel
{
public:
	void operator= (pixel& p);
	void operator= (hsvpixel& hsv);
	void color(BYTE R, BYTE G, BYTE B);
	void color(BYTE X);
	void color(float R, float G, float B);
	BYTE intensity();
	void lighten(UINT i);
	void darken(UINT i);

	BYTE r;
	BYTE g; 
	BYTE b;
};

class hsvpixel
{
public:
	void operator= (hsvpixel& hsv);
	void operator= (pixel& p);

	double h;		// hue				[0,360]
	double s;		// saturation		[0,1]
	double v;		// value/brightness [0,1]
};

//////////////////////////////////////////////////////////////////////
// Type Definitions
//////////////////////////////////////////////////////////////////////

typedef pixel*  imagerow;
typedef pixel** imagematrix;

typedef bool*   boolrow;
typedef bool**  boolmatrix;

#define UNDEFINED -1

// list of all filters
enum filtertype
{
	UNDO,				//   undo

						// POINT PROCESSES
	GRAYINT,			//   convert to grayscale by intensity
	GRAYHUE,			//   convert to grayscale by hue
	GRAYSAT,			//   convert to grayscale by saturation
	HUESAT,				//   alter hue, saturation, and brightness
	EQINTENSITY,		//   make all color intensities (value) equal

						// AREA PROCESSES
	BLUR,				//   normal blur
	MEDIANBLUR,			//   median blur
	CONVOLUTION,		//   convolution filters

						// AREA PROCESSES (ARTISTIC)
	OIL,				//   oil paint effect
	FROSTGLASS,			//   frosted glass effect
	RANDBLUR,			//   random blured squares
	RAINDROPS,			//   looking through a rained on window

						// GEOMETRIC PROCESSES
	FISHEYE,			//   distort to look like fish eye lens was used
	POLAR				//   convert cartesian to polar coordinates
};


//////////////////////////////////////////////////////////////////////
// Image Class Definition 
//////////////////////////////////////////////////////////////////////

class CImage  
{

friend class CFilter; //everything in filter can access private here

public:
	CImage();
	virtual ~CImage();

public:
	// inline functions

	void SetFilename(CString sFilename) { m_filename = sFilename; };
	
	bool OK()							{ return (m_buf != NULL); };
	UINT GetWidth()						{ return m_width;         };
	UINT GetHeight()                    { return m_height;		  };
	CString GetFilename()				{ return m_filename;	  };

public:
	// member functions

	void LoadJPG();
	void LoadBMP();
	void DrawBMP(CDC* dc, CRect rectClient);

	void SaveJPG(CString sFilename);
	void SaveBMP(CString sFilename);

	void ProcessFilter(enum filtertype type);

private:
	// private member variables

	BYTE*   m_buf;
	BYTE*   m_undobuf;
	UINT    m_width;
	UINT    m_height;
	UINT    m_widthDW;
	CString m_filename;
};

//////////////////////////////////////////////////////////////////////
// Filter Class Definition 
//////////////////////////////////////////////////////////////////////

class CFilter  
{
public:
	CFilter(CImage* img, filtertype type);
	virtual ~CFilter();
	void go();
	
private:
	// private member functions

	imagematrix CreateImageMatrix(unsigned int row, unsigned int col);
	void		FreeImageMatrix(imagematrix img, unsigned int row);
	boolmatrix  CreateBoolMatrix(unsigned int row, unsigned int col);
	void        FreeBoolMatrix(boolmatrix bmtx, unsigned int row);
	void		BufferToMatrix(BYTE* buf, imagematrix img);
	void		MatrixToBuffer(imagematrix img, BYTE* buf);
	void		CopyImageBuffer(BYTE* buf, BYTE* bufcopy);

	void		FilterInit();
	pixel		MostFrequentColor(unsigned int x, unsigned int y, unsigned int r, unsigned int I);
	pixel		RandomColor(unsigned int x, unsigned int y, unsigned int r);
	void        MPQ(BYTE* buf, UINT* index, int lb, int ub);
	pixel		MedianPixel(BYTE* buf, pixel* pixbuf, int length);

	bool		grayint();
	bool		grayhue();
	bool		graysat();
	bool		huesat();
	bool		eqintensity();
	bool		blur();
	bool		medianblur();
	bool		convolution();
	bool		oil();
	bool		frostglass();
	bool		randblur();
	bool		raindrops();
	bool		fisheye();	
	bool		polar();

private:
	// private member variables
	
	CImage*		 m_pImage;		// pointer to image calling this filter
	UINT		 m_height;		// image height
	UINT		 m_width;		// image width
	
	filtertype	 m_type;		// what filter?
	imagematrix  m_img;			// original image in matrix form (input to filter)
	imagematrix  m_filt;		// filtered image in matrix form (output from filter)
};


#endif // !defined(AFX_FILTER_H__D53069AA_EA08_4687_9202_7CAF73FE0011__INCLUDED_)
