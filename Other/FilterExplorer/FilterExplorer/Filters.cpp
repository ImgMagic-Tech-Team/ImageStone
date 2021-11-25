/////////////////////////////////////////////////////////////////////////////
//
// FilterExplorer
// Wittenberg University, Springfield, Ohio
// Computer Science Honors Thesis
// 18 April 2001
//
// Filename.....: Filters.cpp
// Compilation..: Microsoft Visual C++ 6.0.
// Description..: implementation  for pixel, hsvpixel, CFilter, CImage classes
//
// Written by...: Jason Waltman
//
/////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "FilterExplorer.h"
#include "Filters.h"

#include "jpegfile.h"
#include "bmpfile.h"
#include "dialogs.h"

#include <math.h>
#include <algorithm>

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// PIXEL PROCESSING SUPPORT FUNCITONS
//////////////////////////////////////////////////////////////////////

void pixel::operator =(pixel &p)
//
// Desc:   RGB pixel assignment
//
{
	r = p.r;
	g = p.g;
	b = p.b;
}

void pixel::operator =(hsvpixel& hsv)
//
// Desc:   Converstion of Hue-Saturation-Value pixel to RGB	
// Pre:    hsv is an HSV pixel
// Post:   the equavalent RGB pixel is returned
// Source: [FOLE90:593], [PCIRGB]
//
{
	double R, G, B;

	if (hsv.s == 0)
	{
		R = hsv.v;
		G = hsv.v;
		B = hsv.v;
	}
	else
	{
		int i;
		double f, p, q, t;

		if (hsv.h == 360)
			hsv.h = 0;
		hsv.h /= 60;			// h now in [0,6)
		i = int(floor(hsv.h));
		f = hsv.h - i;
		p = hsv.v * (1 - hsv.s);
		q = hsv.v * (1 - (hsv.s * f));
		t = hsv.v * (1 - (hsv.s * (1 - f)));
		
		switch (i)
		{
			case 0: R = hsv.v;
				    G = t;
					B = p;
					break;
			case 1: R = q;
				    G = hsv.v;
					B = p;
					break;
			case 2: R = p;
				    G = hsv.v;
					B = t;
					break;
			case 3: R = p;
				    G = q;
					B = hsv.v;
					break;
			case 4: R = t;
				    G = p;
					B = hsv.v;
					break;
			case 5: R = hsv.v;
				    G = p;
					B = q;
		}
	}

	r = (BYTE)(R * 255);
	g = (BYTE)(G * 255);
	b = (BYTE)(B * 255);
}

void pixel::color(BYTE R, BYTE G, BYTE B)
//
// Desc:   Assigns pixel the values R, G, and B
//
{
	r = R;
	g = G;
	b = B;
}

void pixel::color(BYTE X)
//
// Desc:   Makes gray pixel with intensity X
//
{
	r = X;
	g = X;
	b = X;
}

void pixel::color(float R, float G, float B)
//
// Desc:   Colors a pixel given the float values R, G, and B.
//         If the float is < 0 or > 255, the color will be set
//         to 0 or 255 respectively.
//
{
	if (R < 0) R = 0;
	if (R > 255) R = 255;
	if (G < 0) G = 0;
	if (G > 255) G = 255;
	if (B < 0) B = 0;
	if (B > 255) B = 255;

	r = (BYTE)R;
	g = (BYTE)G;
	b = (BYTE)B;
}

BYTE pixel::intensity()
//
// Desc:   Returns the intensity of a RGB pixel.  This is the Luminance (Y)
//         component of the YIQ color model (used in TV displays).  Intensity
//         is most commonly used as the basis for color image analysis.  This
//         intensity 'formula' appears to produce the best results when using
//         this basis.
// Source: [FOLE90:589] 
//
{
	return BYTE(0.3*r + 0.59*g + 0.11*b);
}

void pixel::lighten(UINT i)
//
// Desc:   Lightens a pixel by i.
//
{
	if (r >= 255-i) r = 255;
	else r += i;
	if (g >= 255-i) g = 255;
	else g += i;
	if (b >= 255-i) b = 255;
	else b += i;
}

void pixel::darken(UINT i)
//
// Desc:   Darkens a pixel by i.
//
{
	if (r <= i) r = 0;
	else r -= i;
	if (g <= i) g = 0;
	else g -= i;
	if (b <= i) b = 0;
	else b -= i;
}

void hsvpixel::operator =(hsvpixel& hsv)
//
// Desc:   HSV pixel assignment.
//
{
	h = hsv.h;
	s = hsv.s;
	v = hsv.v;
}

void hsvpixel::operator =(pixel& p)
//
// Desc:   Converstion of RGB pixel to Hue-Saturation-Value pixel
// Pre:    p is a RGB pixel
// Post:   the equavalent HSV pixel is returned
// Source: [FOLE90:592], [PCIIHS]
//
{
	//convert p to range [0,1]
	double r = p.r / 255.0;
	double g = p.g / 255.0;
	double b = p.b / 255.0;

	double Max = max(max(r, g), b);
	double Min = min(min(r, g), b);

	// set value/brightness v
	v = Max;

	// calculate saturation s
	if (Max != 0)
		s = (Max - Min) / Max;
	else
		s = 0;

	// calculate hue h
	if (s == 0)
		h = UNDEFINED;
	else
	{
		double delta = Max - Min;
		if (r == Max)
			h = (g - b) / delta;			// resulting color is between yellow and magenta
		else if (g == Max)
			h = 2 + (b - r) / delta;		// resulting color is between cyan and yellow
		else if (b == Max)
			h = 4 + (r - g) / delta;		// resulting color is between magenta and cyan

		h *= 60;							// convert hue to degrees

		if (h < 0)							// make sure positive
			h += 360;
	}

}

//////////////////////////////////////////////////////////////////////
// CIMAGE Construction/Destruction
//////////////////////////////////////////////////////////////////////

CImage::CImage()
{
	m_buf     = NULL;
	m_undobuf = NULL;
	m_width   = 0;
	m_height  = 0;
	m_widthDW = 0;
	m_filename.Empty();
}

CImage::~CImage()
{
	if (m_buf != NULL)
	{
		delete[] m_buf;
		m_buf = NULL;
	}
	if (m_undobuf != NULL)
	{
		delete[] m_undobuf;
		m_undobuf = NULL;
	}
}

//////////////////////////////////////////////////////////////////////
// CIMAGE Member Functions
//////////////////////////////////////////////////////////////////////

void CImage::LoadJPG()
//
// Desc:   Loads a JPG image from a file into the buffer
// Pre:    m_filename is the name of the file containting the JPG image
// Post:   m_buf contains the file; m_width and m_height are set;
//         m_undobuf has been allocated enough space to hold a copy of an
//         image the size of m_buf.
// Source: [SMALL00]
//
{
	if (m_buf != NULL)		// m_buf is the global buffer
	{
		delete[] m_buf;
		m_buf = NULL;
	}

	if (m_undobuf != NULL)
	{
		delete[] m_undobuf;
		m_undobuf = NULL;
	}

	m_buf = JpegFile::JpegFileToRGB(m_filename, &m_width, &m_height);
	m_undobuf = (BYTE*)new BYTE[m_height * m_width * 3];

	JpegFile::BGRFromRGB(m_buf, m_width, m_height);
	JpegFile::VertFlipBuf(m_buf, m_width * 3, m_height);
}

void CImage::LoadBMP()
//
// Desc:   Loads a BMP image from a file into the buffer
// Pre:    m_filename is the name of the file containting the BMP image
// Post:   m_buf contains the file; m_width and m_height are set;
//         m_undobuf has been allocated enough space to hold a copy of an
//         image the size of m_buf.
// Source: [SMALL00]
//
{
	if (m_buf != NULL)
	{
		delete[] m_buf;
		m_buf = NULL;
	}

	if (m_undobuf != NULL)
	{
		delete[] m_undobuf;
		m_undobuf = NULL;
	}

	BMPFile theBmpFile;
	m_buf = theBmpFile.LoadBMP(m_filename, &m_width, &m_height);
	if ((m_buf == NULL) || (theBmpFile.m_errorText != "OK"))
	{
		AfxMessageBox(theBmpFile.m_errorText, MB_ICONSTOP);
		m_buf = NULL;
		return;
	}

	m_undobuf = (BYTE*)new BYTE[m_height * m_width * 3];

	JpegFile::BGRFromRGB(m_buf, m_width, m_height);
	JpegFile::VertFlipBuf(m_buf, m_width * 3, m_height);
}

void CImage::DrawBMP(CDC* dc, CRect rectClient)
//
// Desc:   Displays m_buf on the screen
// Pre:    dc is a handle to the screen device context; rectClient is the screen size
// Post:   m_buf is displayed, centered, on the screen
// Source: [SMALL00]
//
{
	if (m_buf == NULL)		// if nothing in the buffer, get out
		return;

	if (dc != NULL)
	{
		// center
		int left = max(rectClient.left, ((rectClient.Width()  - (int)m_width)  / 2));
		int top  = max(rectClient.top,  ((rectClient.Height() - (int)m_height) / 2));

		// a 24-bit DIB is DWORD-aligned, vertically flipped and has Red and Blue bytes
		// swapped.  We already did the RGB->BGR and the flip when we read the image, 
		// now do the DWORD-align

		BYTE* tmp;
		tmp = JpegFile::MakeDwordAlignedBuf(m_buf, m_width, m_height, &m_widthDW);

		// set up DIB
		BITMAPINFOHEADER bmiHeader;
		bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
		bmiHeader.biWidth = m_width;
		bmiHeader.biHeight = m_height;
		bmiHeader.biPlanes = 1;
		bmiHeader.biBitCount = 24;
		bmiHeader.biCompression = BI_RGB;
		bmiHeader.biSizeImage = 0;
		bmiHeader.biXPelsPerMeter = 0;
		bmiHeader.biYPelsPerMeter = 0;
		bmiHeader.biClrUsed = 0;
		bmiHeader.biClrImportant = 0;

		// send it to the CDC
		// lines returns the number of lines actually displayed

		int lines = StretchDIBits(
			dc->m_hDC,
			left,
			top,
			bmiHeader.biWidth,
			bmiHeader.biHeight,
			0,
			0,
			bmiHeader.biWidth,
			bmiHeader.biHeight,
			tmp,
			(LPBITMAPINFO)&bmiHeader,
			DIB_RGB_COLORS,
			SRCCOPY);

		delete[] tmp;
	}
}

void CImage::SaveJPG(CString sFilename)
//
// Desc:   Saves the image in m_buf to file sFilename as a JPG
// Pre:    m_buf contains an image; sFilename is the filename to save m_buf to 
// Post:   file has been saved as JPG to sFilename
// Source: [SMALL00]
//
{
	if (m_buf == NULL)
	{
		AfxMessageBox ("No Image to Save!");
		return;
	}

	// we vertical flip for display, undo that.
	JpegFile::VertFlipBuf(m_buf, m_width * 3, m_height);

	// we swap red and blue for display, undo that.
	JpegFile::BGRFromRGB(m_buf, m_width, m_height);

	BOOL ok = JpegFile::RGBToJpegFile(
		sFilename,
		m_buf,
		m_width,
		m_height,
		TRUE,			// save in color
		100);			// quality value [1..100]

	if (!ok)
		AfxMessageBox("Write Error!", MB_ICONSTOP);
	else
	{
		m_filename = sFilename;
		LoadJPG();
	}
}

void CImage::SaveBMP(CString sFilename)
//
// Desc:   Saves the image in m_buf to file sFilename as a BMP
// Pre:    m_buf contains an image; sFilename is the filename to save m_buf to 
// Post:   file has been saved as BMP to sFilename
// Source: [SMALL00]
//
{
	if (m_buf == NULL)
	{
		AfxMessageBox ("No Image to Save!");
		return;
	}

	// image in m_buf is already BGR and vertically flipped
	// so we don't need to do that for this function

	BMPFile theBmpFile;
	theBmpFile.SaveBMP(
		sFilename,
		m_buf,
		m_width,
		m_height);

	if (theBmpFile.m_errorText != "OK")
		AfxMessageBox(theBmpFile.m_errorText, MB_ICONSTOP);
	else
	{
		m_filename = sFilename;
		LoadBMP();
	}
}

void CImage::ProcessFilter(enum filtertype type)
//
// Desc:   Creates a CFilter object attached to this image (i.e.
//         the filter will process this image) of filtertype type.
// Pre:    type is the filtertype you wish to use to filter m_buf
// Post:   If a filter took place, m_buf is the filtered image and m_undobuf
//         is a copy of m_buf before the filter.
//         If there was no filter, m_buf is unchanged and m_undobuf is
//         undefined.
// Source: custom
//
{
	CFilter currentfilter(this, type);
	currentfilter.go();
}


//////////////////////////////////////////////////////////////////////
// CFILTER Notes:
//   CFilter is declared as a friend class in CImage.  Everything in 
//   CFilter can access private members in CImage.  The function
//   descriptions and pre/postconditions below may mention members not
//   declared in CFilter (e.g. m_buf).  These members are declared in
//   CImage. 
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// CFILTER Construction/Destruction
//////////////////////////////////////////////////////////////////////

CFilter::CFilter(CImage* img, filtertype type)
{
	m_pImage = img;
	m_height = img->m_height;
	m_width  = img->m_width;
	m_type   = type;
}

CFilter::~CFilter()
{

}

//////////////////////////////////////////////////////////////////////
// CFILTER Member Functions
//////////////////////////////////////////////////////////////////////

void CFilter::go()
//
// Desc:   Calls the specified filter function
// Pre:    m_type is the filter to be called
// Post:   If a filter took place, m_buf is the filtered image and m_undobuf
//         is a copy of m_buf before the filter.
//         If there was no filter, m_buf is unchanged and m_undobuf is
//         undefined.
// Source: custom
//
{
	bool ok = false;

	if (m_type == UNDO)
		CopyImageBuffer(m_pImage->m_undobuf, m_pImage->m_buf);
	else
	{
		switch (m_type)
		{
			// POINT PROCESSES
			case GRAYINT:				ok = grayint();			break;
			case GRAYHUE:				ok = grayhue();			break;
			case GRAYSAT:				ok = graysat();			break;
			case HUESAT:				ok = huesat();			break;
			case EQINTENSITY:			ok = eqintensity();		break;

			// AREA PROCESSES
			case BLUR:					ok = blur();			break;
			case MEDIANBLUR:			ok = medianblur();		break;
			case CONVOLUTION:			ok = convolution();		break;

			// AREA PROCESSES (ARTISTIC)
			case OIL:					ok = oil();				break;
			case FROSTGLASS:			ok = frostglass();		break;
			case RANDBLUR:				ok = randblur();		break;
			case RAINDROPS:				ok = raindrops();		break;

			// GEOMETRIC PROCESSES
			case FISHEYE:				ok = fisheye();			break;
			case POLAR:					ok = polar();			break;
			
			default: ok = false;
		}

		if (ok)
		{
			// Allow undo
			CopyImageBuffer(m_pImage->m_buf, m_pImage->m_undobuf);
			CWnd* pMain = AfxGetMainWnd();
			if (pMain != NULL)
			{
				CMenu* pMenu = pMain->GetMenu();
				pMenu->EnableMenuItem(ID_UNDO, MF_ENABLED);
			}

			MatrixToBuffer(m_filt, m_pImage->m_buf);	// Send our filtered image to the buffer to display
			FreeImageMatrix(m_img, m_height);			// Free image matrix memory
			FreeImageMatrix(m_filt, m_height);			// "
		}
	}
}


//////////////////////////////////////////////////////////////////////
// CFILTER Private Member Functions
//////////////////////////////////////////////////////////////////////

// IMAGE MATRIX-BUFFER SUPPORT FUNCTIONS /////////////////////////////

imagematrix CFilter::CreateImageMatrix(unsigned int row, unsigned int col)
//
// Desc:   Allocates memory for an image matrix of pixels, of size row by col
// Pre:    row are col are size for matrix allocation
// Post:   Pointer to allocated memory returned.
// Source: modified from [PITA00]
//
{
	// dynmaic allocation of a two-dimensional array
	imagematrix img = NULL;
	img = (imagematrix)malloc(row * sizeof(imagerow));
	
	if (img == NULL)
		return(NULL);

	for (unsigned int i = 0; i < m_height; i++)
	{
		img[i] = (imagerow)malloc(col * sizeof(pixel));
		if (img[i] == NULL)
		{
			FreeImageMatrix(img, row);
			return(NULL);
		}
	}
	return(img);
}

void CFilter::FreeImageMatrix(imagematrix img, unsigned int row)
//
// Desc:   Deallocates memory from an image matrix of pixels, of row rows
// Pre:    img was allocated by CreateImageMatrix(...); row is number
//         of rows in imagematrix img
// Post:   memory deallocated
// Source: modified from [PITA00]
//
{
	for (unsigned int i = 0; i < row; i++)
		free(img[i]);
	free(img);
}

boolmatrix  CFilter::CreateBoolMatrix(unsigned int row, unsigned int col)
//
// Desc:   Allocates memory for an matrix of bool, of size row by col
// Pre:    row are col are size for matrix allocation
// Post:   Pointer to allocated memory returned.
// Source: modified from [PITA00]
//
{
	// dynmaic allocation of a two-dimensional array
	boolmatrix bmtx = NULL;
	bmtx = (boolmatrix)malloc(row * sizeof(boolrow));
	
	if (bmtx == NULL)
		return(NULL);

	for (unsigned int i = 0; i < m_height; i++)
	{
		bmtx[i] = (boolrow)malloc(col * sizeof(bool));
		if (bmtx[i] == NULL)
		{
			FreeBoolMatrix(bmtx, row);
			return(NULL);
		}
	}
	return(bmtx);
}

void CFilter::FreeBoolMatrix(boolmatrix bmtx, unsigned int row)
//
// Desc:   Deallocates memory from an bool matrix, of row rows
// Pre:    bmtx was allocated by CreateBoolMatrix(...); row is number
//         of rows in boolmatrix bmtx
// Post:   memory deallocated
// Source: modified from [PITA00]
//
{
	for (unsigned int i = 0; i < row; i++)
		free(bmtx[i]);
	free(bmtx);
}

void CFilter::BufferToMatrix(BYTE* buf, imagematrix img)
//
// Desc:   Converts string of BYTEs (buf) to imagematrix img 
// Pre:    buf != NULL, img created from CreateImageMatrix,
//         m_height and m_width are dimensions of imagematrix img
// Post:   img contains RGB pixels translated from buf
// Source: custom; [SMAL00], [PITA00]
//
{
	UINT col, row;
	for (row=0; row < m_height; row++)
	{
		for (col=0; col < m_width; col++)
		{
			LPBYTE pRed, pGrn, pBlu;
			pBlu = buf + row * m_width * 3 + col * 3;
			pGrn = buf + row * m_width * 3 + col * 3 + 1;
			pRed = buf + row * m_width * 3 + col * 3 + 2;
			img[row][col].r = *pRed;
			img[row][col].g = *pGrn;
			img[row][col].b = *pBlu;
		}
	}
}

void CFilter::MatrixToBuffer(imagematrix img, BYTE* buf)
//
// Desc:   Converts imagematric img to string of BYTEs (buf) 
// Pre:    img contains an image of pixels, buf != NULL,
//         m_height and m_width are dimensions of imagematrix img
// Post:   buf contains BTYEs translated from img pixels
// Source: custom; [SMAL00], [PITA00]
//
{
	UINT col, row;
	for (row = 0; row < m_height; row++)
	{
		for (col = 0; col < m_width; col++)
		{
			BYTE Red, Grn, Blu;
			Red = img[row][col].r;
			Grn = img[row][col].g;
			Blu = img[row][col].b;
			buf[row * m_width * 3 + col * 3] = Blu;
			buf[row * m_width * 3 + col * 3 + 1] = Grn;
			buf[row * m_width * 3 + col * 3 + 2] = Red;
		}
	}
}

void CFilter::CopyImageBuffer(BYTE* buf, BYTE* bufcopy)
//
// Desc:   Copies buf to bufcopy. 
// Pre:    buf, buffcopy != NULL both must have been allocated 
//         the same memory space (this is done in CImage::LoadJPG or 
//         in CImage::LoadBMP).
// Post:   Both buf and bufcopy exist on successful return.  If buf == NULL
//         upon entry, buf and bufcopy are undefined on return.
// Source: custom
//
{
	if (buf==NULL)
		return;

	if (bufcopy==NULL)
		return;

	int size = m_height * m_width * 3;

	for (int i = 0; i < size; i++)
	{
		bufcopy[i] = buf[i];
	}
	return;
}

// FILTER SUPPORT FUNCTIONS ///////////////////////////////////////

void CFilter::FilterInit()
//
// Desc:   Prepares m_img and m_filt so the image in the buffer m_buf can be filtered
//         to the matrix for m_filt
// Pre:    m_buf != NULL, m_height and m_width are size of image in m_buf
// Post:   m_img created from m_buf, m_filt created the same size as m_img
//         but not initialized.
// Source: custom
//
{
	// place the buffer in a dynamic image matrix
	m_img = CreateImageMatrix(m_height, m_width);
	BufferToMatrix(m_pImage->m_buf, m_img);

	// create blank 'to-be-filtered' image
	m_filt = CreateImageMatrix(m_height, m_width);
}

pixel CFilter::MostFrequentColor(unsigned int x, unsigned int y, unsigned int r, unsigned int I)
//
// Desc:   Determines the most frequently appearing color in a sub-matrix of m_img
//         according to the pre/postconditions below.
// Pre:    (x,y) is the 'center' pixel in a sub-matrix of m_img with radius r.
//         I is the number of different intensities to distinguish in the sub-matrix.
// Post:   The 'average' color of the pixels with the most frequently appearing
//         intensity in the sub-matrix of pixels centered at (x,y) with radius r
//		   in m_img.
// Source: modified for color pixels from [HOLZ88:111-112]; [SEUL00:52], [FOLE90:589-591]
//
{
	int i, j;
	pixel q;
	BYTE intensity;
	BYTE MAXINTENSITY = I;
	double scale = MAXINTENSITY / 255.0;

	// dynamic array allocations:
	//    e.g. BYTE intensitycount[I+1];

	BYTE* intensitycount = NULL;
	intensitycount = (BYTE*)malloc((I+1) * sizeof(BYTE));	// index = intensity
															// value at index = count;
	UINT* averagecolorR = NULL;
	averagecolorR = (UINT*)malloc((I+1) * sizeof(UINT));	
	
	UINT* averagecolorG = NULL;
	averagecolorG = (UINT*)malloc((I+1) * sizeof(UINT));
	
	UINT* averagecolorB = NULL;
	averagecolorB = (UINT*)malloc((I+1) * sizeof(UINT));

	for (i = 0; i <= MAXINTENSITY; i++)
		intensitycount[i] = 0;								// set all counters to 0
	
	for (i = int(x-r); i <= int(x+r); i++)
		for (j = int(y-r); j <= int(y+r); j++)
		{
			if (i >= 0 && i < (int)m_height && j >= 0 && j < (int)m_width) 
			{
				q = m_img[i][j];
				intensity = q.intensity() * scale;			// find intensity
				intensitycount[intensity]++;
				
				if (intensitycount[intensity] == 1)
				{
					averagecolorR[intensity] = q.r;
					averagecolorG[intensity] = q.g;
					averagecolorB[intensity] = q.b;
				}
				else
				{
					averagecolorR[intensity] += q.r;		// keep a running total
					averagecolorG[intensity] += q.g;		//   we'll divide later
					averagecolorB[intensity] += q.b;
				}
			}
		}

	// find max intensity
	
	intensity = 0;
	int maxinstance = 0;

	for (i = 0; i <= MAXINTENSITY; i++)
	{
		if (intensitycount[i] > maxinstance)
		{
			intensity = i;
			maxinstance = intensitycount[i];
		}
	}

	pixel mostfrequent;
	mostfrequent.r = averagecolorR[intensity] / maxinstance;
	mostfrequent.g = averagecolorG[intensity] / maxinstance;
	mostfrequent.b = averagecolorB[intensity] / maxinstance;

	free(intensitycount);
	free(averagecolorR);
	free(averagecolorG);
	free(averagecolorB);

	return mostfrequent;
}

pixel CFilter::RandomColor(unsigned int x, unsigned int y, unsigned int r)
//
// Desc:   Chooses a random color from those appearing in a sub-matrix of m_img
//         according to the pre/postconditions below.
// Pre:    (x,y) is the 'center' pixel in a sub-matrix of m_img with radius r.
// Post:   The 'average' color of the pixels of a random intensity in the
//         sub-matrix of pixels centered at (x,y) with radius r.  Intensity
//         is chosen by giving weight to the more common intensities.
// Source: modified from MostFrequentColor(...); [HOLZ88:111-112],
//         [SEUL00:52], [FOLE90:589-591]
//
{
	int i, j;
	pixel q;
	BYTE intensity;
	const BYTE MAXINTENSITY = 255;
	
	BYTE intensitycount[MAXINTENSITY+1];	// index = intensity, value = count;
	UINT averagecolorR[MAXINTENSITY+1];
	UINT averagecolorG[MAXINTENSITY+1];
	UINT averagecolorB[MAXINTENSITY+1];

	for (i = 0; i <= MAXINTENSITY; i++)
	{
		intensitycount[i] = 0;	// set all counters to 0
	}
	
	int numint = 0;

	for (i = int(x-r); i <= int(x+r); i++)
		for (j = int(y-r); j <= int(y+r); j++)
		{
			if (i >= 0 && i < (int)m_height && j >= 0 && j < (int)m_width) 
			{
				q = m_img[i][j];
				intensity = q.intensity();
				intensitycount[intensity]++;
				numint++;

				if (intensitycount[intensity] == 1)
				{
					averagecolorR[intensity] = q.r;
					averagecolorG[intensity] = q.g;
					averagecolorB[intensity] = q.b;
				}
				else
				{
					averagecolorR[intensity] += q.r;
					averagecolorG[intensity] += q.g;
					averagecolorB[intensity] += q.b;
				}

			}
		}
	
	int randnum, count, indx;
	do
	{
		// weighted random
		randnum = int((rand()+1) * ((double)numint / (RAND_MAX+1)));
		count = 0;
		indx = 0;
		do
		{
			count += intensitycount[indx]; 
			indx++;
		} while (count < randnum);

		intensity = indx-1;

	} while (intensitycount[intensity] == 0);

	pixel randpix;
	randpix.r = averagecolorR[intensity] / intensitycount[intensity];
	randpix.g = averagecolorG[intensity] / intensitycount[intensity];
	randpix.b = averagecolorB[intensity] / intensitycount[intensity];

	return randpix;
}

void CFilter::MPQ(BYTE* buf, UINT* index, int lb, int ub)
//
// Desc:   Quicksort implementation for MedianPixel(...)
// Source: [PARS95:98-103]
//
{
	int i, j;
	BYTE pivot;

	if (ub > lb)
	{
		j = lb - 1;					// make the partitions
		pivot = buf[index[ub]];
		for (i = lb; i <= ub; i++)	// main loop
		{
			if (buf[index[i]] <= pivot)
			{
				j++;
				if (i != j)
					std::swap(index[i], index[j]);
			}
		}
		MPQ(buf, index, lb, j-1);
		MPQ(buf, index, j+1, ub);
	}
}

pixel CFilter::MedianPixel(BYTE* buf, pixel* pixbuf, int length)
//
// Desc:   Modification of the Quicksort sorting algorithm.
// Pre:	   buf and pixbuf != NULL; buf is intensities of the corresponding pixels
//         in pixbuf; length is number of entries in both buf and pixbuf to use
//         to find the median (beginning with element 0).
// Post:   Returns the 'median' pixel (based on intensity) of pixbuf.
//         buf and pixbuf are unmodified.
// Source: largely custom; [PARS95:98-103], [PITA00:141-143]
// 
{
	// dynamic array allocation
	UINT* index = NULL;
	index = (UINT*)malloc(length * sizeof(UINT));

	int x;

	for (x = 0; x < length; x++)		// initialize index with indicies of buf/pixbuf
		index[x] = x;					// we will sort the indicies, not the elements 
										// of buf and pixbuf
	UINT lb = 0;
	UINT ub = length - 1;

	MPQ(buf, index, lb, ub);			// call quicksort

	pixel p = pixbuf[index[length/2]];	// use index to get the median from pixbuf

	free(index);

	return p;
}

// FILTERS ////////////////////////////////////////////////////////

bool CFilter::grayint()
//
// Desc:   Convert to grayscale using color intensity
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [SEUL00:51-55], [FOLE90:589-591]
//
{
	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;
	BYTE x;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			x = m_img[i][j].intensity();
			m_filt[i][j].color(x);

			pixelsProcessed++;			
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::grayhue()
//
// Desc:   Convert to grayscale using color hue
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [SEUL00:51-55], [FOLE90:592]
//
{
	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;
	BYTE x;
	hsvpixel hsvpix;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			hsvpix = m_img[i][j];
			x = (BYTE)(hsvpix.h * (255.0 / 360));
			m_filt[i][j].color(x);

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::graysat()
//
// Desc:   Convert to grayscale using color saturation
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [SEUL00:51-55], [FOLE90:592]
//
{
	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;
	BYTE x;
	hsvpixel hsvpix;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			hsvpix = m_img[i][j];
			x = (BYTE)(hsvpix.s * 255);
			m_filt[i][j].color(x);

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::huesat()
//
// Desc:   Modify Hue, Saturation, and Value (Brightness) of the image.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [FOLE90:590-593]
//
{
	// Call Dialog
	CHueSatDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;
	hsvpixel hsvpix;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			hsvpix = m_img[i][j];

			if (dlg.m_bColorize)
				hsvpix.h = dlg.m_iHue + 180;
			else
			{
				hsvpix.h += dlg.m_iHue;
				if (hsvpix.h < 0)
					hsvpix.h += 360;
				else if (hsvpix.h > 360)
					hsvpix.h -=360;
			}

			if (dlg.m_iLight < 0)
				hsvpix.v = (dlg.m_iLight + 100) * (hsvpix.v / 100.0);
			else if (dlg.m_iLight > 0)
				hsvpix.v = ((dlg.m_iLight) * ((1 - hsvpix.v) / 100.0)) + hsvpix.v;

			if (dlg.m_iSat < 0)
				hsvpix.s = (dlg.m_iSat + 100) * (hsvpix.s / 100.0);
			else if (dlg.m_iSat > 0)
				hsvpix.s = ((dlg.m_iSat) * ((1 - hsvpix.s) / 100.0)) + hsvpix.s;

			m_filt[i][j] = hsvpix;

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::eqintensity()
//
// Desc:   Convert every pixel to the same intensity (value/brightness).  Theoretically,
//         based on color theory, this should produce a 'pleasing' color scheme given the right
//         proportion of color (hue).
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (custom); idea from Edward Charney, Associate Professor of Art, Wittenberg Univ.
//
{
	// Call Dialog
	CEqIntensityDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;
	hsvpixel hsvpix;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			hsvpix = m_img[i][j];
			hsvpix.v = dlg.m_iIntensity / 100.0;
			m_filt[i][j] = hsvpix;

			pixelsProcessed++;
			
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::blur()
//
// Desc:   Blur Filter.  Simple average of all surrounding pixels in a given radius
//         to the center pixel.  Also called moving average.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: modified from [PITA00:122-124]
//
{
	// Call Dialog
	CBlurDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	float R, G, B;
	int i, j, k, l, x, y;

	int numpixels;				// number of pixels in the 'window' for a particular window
								//   will be smaller than normal for pixels around the border

	int RAD = dlg.m_nRadius;
	int circumference = RAD * 2;
	int ksq;					// k^2
	double  r;					// polar coordinate radius

	for (i = 0; i < (int)m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < (int)m_width; j++)
		{
			R = G = B = 0;
			numpixels = 0;

			for (k = -RAD; k <= RAD; k++)
			{
				if (!dlg.m_bFast) ksq = k*k;
				for (l = -RAD; l <= RAD; l++)
				{
					// convert this square to polar
					if (!dlg.m_bFast) r = sqrt(ksq + l*l); 

					// if we're inside R (i.e. inside the circle, inside the square) do...
					if ((dlg.m_bFast) || (!dlg.m_bFast && r <= RAD))
					{
						x = i + k;
						y = j + l;

						if (x >= 0 && x < (int)m_height && y >= 0 && y < (int)m_width)
						{
							R += (float)m_img[x][y].r;
							G += (float)m_img[x][y].g;
							B += (float)m_img[x][y].b;
							numpixels++;
						}
					}
				}
			}
			m_filt[i][j].color(
				(BYTE)(R/numpixels), (BYTE)(G/numpixels), (BYTE)(B/numpixels));

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::medianblur()
//
// Desc:   Median blur filter.  For each pixel in the original image, assigns
//         the cooresponing pixel in the filtered image to the pixel with the 
//         median intensity of the surrounding pixels.  Radius determined by 
//         CMedianBlurDlg.  Similar to blur() but preserves edges better.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: modified from [PITA00:141-143]; [LIND91:378-382]
//
{
	// Call Dialog
	CMedianBlurDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	float R, G, B;
	int i, j, k, l, x, y;
	BYTE intensity;
	pixel q;

	int numpixels;	// number of pixels in the 'window' for a particular window
	                //   will be smaller than normal for pixels around the border
	
	int circumference = dlg.m_iRadius * 2 + 1;
	int circumference2 = circumference * circumference;

	pixel* colorsbuf = NULL;
	colorsbuf = (pixel*)malloc(circumference2 * sizeof(pixel));

	BYTE* intbuf = NULL;
	intbuf = (BYTE*)malloc(circumference2 * sizeof(BYTE));

	for (i = 0; i < (int)m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < (int)m_width; j++)
		{
			R = G = B = 0;
			numpixels = 0;
			for (k = 0; k < circumference; k++)
			{
				for (l = 0; l < circumference; l++)
				{
					x = i + k - (dlg.m_iRadius);
					y = j + l - (dlg.m_iRadius);

					if (x >= 0 && x < (int)m_height && y >= 0 && y < (int)m_width)
					{
						q = m_img[x][y];
						intensity = q.intensity();
						intbuf[circumference*k+l] = intensity;
						colorsbuf[circumference*k+l] = m_img[x][y];
						numpixels++;
					}
				}
			}
			m_filt[i][j] = MedianPixel(intbuf, colorsbuf, numpixels);

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	free(colorsbuf);
	free(intbuf);

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::convolution()
//
// Desc:   Custom convolution filter.  For each pixel in the original image, 
//         assigns the cooresponing pixel in the filtered image to the pixel
//         with a weighted average of the surrounding pixels (the convolution
//         kernel).  The kernel is determined by CConvolutionDlg.  
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (implementation largely custom); [PITA00:122-124], [LIND91:367-370],
//         [DAVI98:60-62], [BORO97]
//
{
	// Call Dialog
	CConvolutionDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	float hcoe[5][5];
	hcoe[0][0] = (float)dlg.m_iPos11;
	hcoe[0][1] = (float)dlg.m_iPos12;
	hcoe[0][2] = (float)dlg.m_iPos13;
	hcoe[0][3] = (float)dlg.m_iPos14;
	hcoe[0][4] = (float)dlg.m_iPos15;
	hcoe[1][0] = (float)dlg.m_iPos21;
	hcoe[1][1] = (float)dlg.m_iPos22;
	hcoe[1][2] = (float)dlg.m_iPos23;
	hcoe[1][3] = (float)dlg.m_iPos24;
	hcoe[1][4] = (float)dlg.m_iPos25;
	hcoe[2][0] = (float)dlg.m_iPos31;
	hcoe[2][1] = (float)dlg.m_iPos32;
	hcoe[2][2] = (float)dlg.m_iPos33;
	hcoe[2][3] = (float)dlg.m_iPos34;
	hcoe[2][4] = (float)dlg.m_iPos35;
	hcoe[3][0] = (float)dlg.m_iPos41;
	hcoe[3][1] = (float)dlg.m_iPos42;
	hcoe[3][2] = (float)dlg.m_iPos43;
	hcoe[3][3] = (float)dlg.m_iPos44;
	hcoe[3][4] = (float)dlg.m_iPos45;
	hcoe[4][0] = (float)dlg.m_iPos51;
	hcoe[4][1] = (float)dlg.m_iPos52;
	hcoe[4][2] = (float)dlg.m_iPos53;
	hcoe[4][3] = (float)dlg.m_iPos54;
	hcoe[4][4] = (float)dlg.m_iPos55;

	float R, G, B;
	int a, b, i, j, k, l, x, y;
	
	const int radius = 2;
	const int circumference = 4;	// actually 5, but the algorithm takes care of the 'center'
									// pixel in implementation; this notion is taken from the 
									// blur() filter
	
	float sum = 0;					// convolution kernal sum
	for (a = 0; a < 5; a++)
		for (b = 0; b < 5; b++)
			sum += hcoe[a][b];

	if (sum <= 0)
		sum = 1;
	else
	{
		for (a = 0; a < 5; a++)
			for (b = 0; b < 5; b++)
				hcoe[a][b] /= sum;
	}


	for (i = 0; i < (int)m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < (int)m_width; j++)
		{
			R = G = B = 0;
			for (k = 0; k <= circumference; k++)
			{
				for (l = 0; l <= circumference; l++)
				{
					x = i + k - radius;
					y = j + l - radius;

					if (x >= 0 && x < (int)m_height && y >= 0 && y < (int)m_width)
					{
						R += (float)m_img[x][y].r * hcoe[k][l];
						G += (float)m_img[x][y].g * hcoe[k][l];
						B += (float)m_img[x][y].b * hcoe[k][l];
					}
				}
			}
			m_filt[i][j].color(R, G, B);

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::oil()
//
// Desc:   Oil paint effect filter.  For each pixel, sets the cooresponding pixel
//         in the filtered image to the 'most frequently appearing color' of the
//         surrounding pixels.  Calls MostFrequentColor(..).  Radius (brush size)
//         determined [1..5] from COilDlg.  Smoothness [10..255] also determined
//         from COilDlg.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: [HOLZ88:46-47, 111-112]
//
{
	// Call Dialog
	COilDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			m_filt[i][j] = MostFrequentColor(i, j, dlg.m_iBrush, dlg.m_iSmooth);
			pixelsProcessed++;
			
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::frostglass()
//
// Desc:   Produces an image that appears the viewer is looking through
//         frosted glass.  For each pixel, sets the cooresponding pixel
//         in the filtered image to a 'weighted (by intensity) random color'
//         of the surrounding pixels.  Calls RandomColor(...).  Radius (frost
//         amount) determined [1..10] from CFrostGlassDlg.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [HOLZ88:111-112]
//
{
	// Call Dialog
	CFrostGlassDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j;

	for (i = 0; i < m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < m_width; j++)
		{
			m_filt[i][j] = RandomColor(i, j, dlg.m_iFrost);
			pixelsProcessed++;
			
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::randblur()
//
// Desc:   Radomly blurs square sections in the image.  User chooses options
//         from CRandBlurDlg: m_iSize is dimension of each square, m_iRadius is the
//         blur radius (how much each block is blurred), m_iAmount is how many
//         sqaures sections to blur.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (largely custom); [PITA00:122-124]
//
{
	// Call Dialog
	CRandBlurDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	pdlg.SetRange(0, dlg.m_iAmount);
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	unsigned int i, j, k, l;
	unsigned int x, y;						// beginning coord (random)
	unsigned int ex, ey;					// end coord (from beginning)

	int rad = dlg.m_iRadius;				// blur radius (bluriness)
	int RAD = rad * rad;					// total num pix processed per block 
	int rad2 = rad / 2;						// half of the radius

	float R, G, B;

	// make copy of image
	for (i = 0; i < m_height; i++)
		for (j = 0; j < m_width; j++)
			m_filt[i][j] = m_img[i][j];

	// and blur some of it
	for (int numblurs = 0; numblurs < dlg.m_iAmount; numblurs++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		do
		{
			x = int(rand() * ((double)m_height / RAND_MAX));
			y = int(rand() * ((double)m_width / RAND_MAX));
			ex = x + dlg.m_iSize - 1;
			ey = y + dlg.m_iSize - 1;

		} while (ex >= m_height || ey >= m_width); 


		for (i = x + rad2; i < ex - rad2; i++)
			for (j = y + rad2; j < ey - rad2; j++)
			{
				R = G = B = 0;
				for (k = 0; (int)k < rad; k++)
				{
					for (l = 0; (int)l < rad; l++)
					{
						R += (float)m_img[i+k-rad2][j+l-rad2].r;
						G += (float)m_img[i+k-rad2][j+l-rad2].g;
						B += (float)m_img[i+k-rad2][j+l-rad2].b;
					}
				}
				m_filt[i][j].color(BYTE(R/RAD), BYTE(G/RAD), BYTE(B/RAD)); 
			}
			pdlg.StepIt();
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::raindrops()
//
// Desc:   Produces effect of water drops on a surface.  Number of water drops
//         (dlg.m_iAmount) and maximum size (dlg.m_iSize) are determined from
//         CRaindropsDlg.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: (idea custom); implementation issues: [PITA00:122-124], [HOLZ88:60-61],
//         [DEVE01], [CLAE97]
//
{
	// Call Dialog
	CRaindropsDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	pdlg.SetRange(0, dlg.m_iAmount);
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	int		i, j, k, l, m, n;			// for loop processing (image coordinates)

	int		x, y;						// center coord of raindrop (random)
	double  r, a;						// polar coordinates of raindrop (radius, angle)
	double  oldr;						// polar radius before fish eye transformation

	bool	findanother = false;		// used to find 'good' random coordinate of raindrop
	int		count = 0;

	int		SIZE = dlg.m_iSize;			// size of largest raindrop (from dialog)
	int		size;						// size of current raindrop
	int		size2;						// half of the current raindrop size
	int		R;							// maxium raindrop radius (same as size2)

	double  w = 0.4;					// fish eye coefficients
	double  s;

	int		blurrad;					// blur radius (half size of blur blur kernel
	float   red, gre, blu;				// red, green, blue values used in blur of raindrop
	int		num;						// num of pixels in blur kernel 

	boolmatrix bmtx;
	bmtx = CreateBoolMatrix(m_height, m_width);

	// set up bool matrix (to test raindrop location)
	//    and make copy of image
	for (i = 0; i < m_height; i++)
		for (j = 0; j < m_width; j++)
		{
			bmtx[i][j] = false;
			m_filt[i][j] = m_img[i][j];
		}

	// do raindrops
	for (int numblurs = 0; numblurs < dlg.m_iAmount; numblurs++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		// determine size
		size = rand() * ((double)(SIZE-5) / RAND_MAX) + 5;
		size2 = size / 2;
		R = size2;
		s = R / log(w*R+1);

		// find random coord to make raindrop
		count = 0;
		do
		{
			findanother = false;
			x = int(rand() * ((double)(m_height-1) / RAND_MAX));
			y = int(rand() * ((double)(m_width-1) / RAND_MAX));
			
			if (bmtx[x][y])
				findanother = true;
			else
			{
				for (i = x-size2; i <= x+size2; i++)
					for (j = y-size2; j <= y+size2; j++)
					{
						if (i >= 0 && i < (int)m_height && j >= 0 && j < (int)m_width)
							if (bmtx[i][j])
								findanother = true;
					}
			}
			count++;

		} while (findanother && (count < 10000));

		if (count >= 10000)
		{
			numblurs = dlg.m_iAmount;	// this will kill the for loop
			pdlg.SetPos(100);
			break;						// stop us from doing this raindrop
		}


		// do fisheye for square of sides size around that point

		// double for loop around center of raindrop
		for (i = -1 * size2; i < size - size2; i++)
		{
			for (j = -1 * size2; j < size - size2; j++)
			{
				// convert this square to polar
				r = sqrt(i*i + j*j);
				a = atan2((float)i, j);			// calculates arctan(i/j)

				// if we're inside R (i.e. inside the circle, inside the square) do...
				if (r <= R)
				{
					oldr = r;
					// make transformation using Basu and Licardie model [DEVE01]
					r = (exp(r/s)-1)/w;

					// convert to cartesian; displace to (y,x)
					k = x + int(r * sin(a));
					l = y + int(r * cos(a));

					// double for loop around raindrop to (y,x)
					m = x + i;
					n = y + j;

					if (k >= 0 && k < (int)m_height && l >= 0 && l < (int)m_width)
						if (m >= 0 && m < (int)m_height && n >= 0 && n < (int)m_width)
						{
							m_filt[m][n] = m_img[k][l];
							bmtx[m][n] = true;

							if (dlg.m_bHighlight) // if the user wants highlight/shadow
							{				
								if (oldr >= 0.9 * R)
								{
									if ((a <= 0) && (a > -2.25))
										m_filt[m][n].darken(80);
									else if ((a <= -2.25) && (a > -2.5))
										m_filt[m][n].darken(40);
									else if (( a <= 0.25) && ( a > 0))
										m_filt[m][n].darken(40);
								}
								else if (oldr >= 0.8 * R)
								{
									if ((a <= -0.75) && (a > -1.50))
										m_filt[m][n].darken(40);
									else if ((a <= 0.10) && (a > -0.75))
										m_filt[m][n].darken(30);
									else if ((a <= -1.50) && (a > -2.35))
										m_filt[m][n].darken(30);
								}
								else if (oldr >= 0.7 * R)
								{
									if ((a <= -0.10) && (a > -2.0))
										m_filt[m][n].darken(20);
									else if ((a <= 2.50) && (a > 1.90))
										m_filt[m][n].lighten(60);
								}
								else if (oldr >= 0.6 * R)
								{
									if ((a <= -0.50) && (a > -1.75))
										m_filt[m][n].darken(20);
									else if ((a <= 0) && ( a > -0.25))
										m_filt[m][n].lighten(20);
									else if ((a <= -2.0) && (a > -2.25))
										m_filt[m][n].lighten(20);
								}
								else if (oldr >= 0.5 * R)
								{
									if ((a <= -0.25) && (a > -0.50))
										m_filt[m][n].lighten(30);
									else if ((a <= -1.75 ) && (a > -2.0))
										m_filt[m][n].lighten(30);
								}
								else if (oldr >= 0.4 * R)
								{
									if ((a <= -0.5) && (a > -1.75))
										m_filt[m][n].lighten(40);
								}
								else if (oldr >= 0.3 * R)
								{
									if ((a <= 0) && (a > -2.25))
										m_filt[m][n].lighten(30);
								}
								else if (oldr >= 0.2 * R)
								{
									if ((a <= -0.5) && (a > -1.75))
										m_filt[m][n].lighten(20);
								}
							}
						}
				}
			}
		}

		if (dlg.m_bHighlight) // don't blur the water unless using highlight/shadow
		{
			blurrad = size / 25 + 1;
			for (i = -1 * size2 - blurrad; i < size - size2 + blurrad; i++)
			{
				for (j = -1 * size2 - blurrad; j < size - size2 + blurrad; j++)
				{
					// convert this square to polar
					r = sqrt(i*i + j*j);
					if (r <= R*1.1)
					{
						red = gre = blu = 0;
						num = 0;
						for (k = -blurrad; k < blurrad + 1; k++)
						{
							for (l = -blurrad; l < blurrad + 1; l++)
							{
								m = x+i+k;
								n = y+j+l;
								if (m >= 0 && m < (int)m_height && n >= 0 && n < (int)m_width)
								{
									red += m_filt[m][n].r;
									gre += m_filt[m][n].g;
									blu += m_filt[m][n].b;
									num++;
								}
							}
						}
						m = x+i;
						n = y+j;
						if (m >= 0 && m < (int)m_height && n >= 0 && n < (int)m_width)
							m_filt[m][n].color(BYTE(red/num), BYTE(gre/num), BYTE(blu/num));
					}
				}
			}
		}
		
		pdlg.StepIt();
	}

	FreeBoolMatrix(bmtx, m_height);

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::fisheye()
//
// Desc:   Makes center of image appear as it was pushed out by a sphere or pushed
//         in by a cone.  User options from CFishEyeDlg: m_bInverse == true, 'pushed
//         in by cone' effect; m_iBackround, 1 -> leave background alone, 2 -> make 
//         background white, 3 3-> make background black; m_iCurvature, amount to push
//         out or in.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: [HOLZ88:34-35,60-61], [DEVE01], [CLAE97]
//
{
	// Call Dialog
	CFishEyeDlg dlg;
	if (dlg.DoModal() != IDOK)
		return false;

	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////
	
	int			 i, j;								// cartesian coordinates
	int			 x, y;								// converted cartesian coordinates
	
	double		 r, a;								// polar coordinates (radius, angle)
	unsigned int R  = (min(m_width, m_height)) / 2; // maxium radius of polar coordinates
	
	double		 w = 0.001 * dlg.m_iCurvature;		// curvature [0.001,0.1]
													//   m_iCurvature will return [1,100]
	double		 s = R / log(w*R+1);				// transformation coefficient
													//   set according to largest radius
													//   and curvature w

	int w2 = m_width  / 2;
	int h2 = m_height / 2;

	enum background {leave, white, black};
	int  backgroundtype = dlg.m_iBackground;
	BOOL inverse = dlg.m_bInverse;
	
	// for loops with origin in center of image
	// [i,j] in terms of filtered image

	for (i = -1*h2; i < ((int)m_height-h2); i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}
		for (j = -1*w2; j < ((int)m_width-w2); j++)
		{
			// convert to polar
			r = sqrt(i*i + j*j);
			a = atan2((float)i, j);		// calculates arctan(i/j)

			// if we're inside R (i.e. inside the circle) do...
			if (r <= R)
			{
				// make transformation using Basu and Licardie model [DEVE01]
				if (!inverse)
					r = (exp(r/s)-1)/w;
				else
					r = s * log(1+w*r);
		
				// convert back to cartesian
				x = int  (r * cos(a));
				y = int  (r * sin(a));

				// move origin back to bottom left
				x += w2;
				y += h2;

				m_filt[i+h2][j+w2].b = m_img[y][x].b;
				m_filt[i+h2][j+w2].g = m_img[y][x].g;
				m_filt[i+h2][j+w2].r = m_img[y][x].r;

			}
			
			// if we're outside R, do not curve
			else
			{
				switch (backgroundtype)
				{
					case leave:		m_filt[i+h2][j+w2].b = m_img[i+h2][j+w2].b;
									m_filt[i+h2][j+w2].g = m_img[i+h2][j+w2].g;
									m_filt[i+h2][j+w2].r = m_img[i+h2][j+w2].r;
									break;
					case white:		m_filt[i+h2][j+w2].b = 255;
									m_filt[i+h2][j+w2].g = 255;
									m_filt[i+h2][j+w2].r = 255;
									break;
					default:		m_filt[i+h2][j+w2].b = 0;
									m_filt[i+h2][j+w2].g = 0;
									m_filt[i+h2][j+w2].r = 0;	
				}
			}

			pixelsProcessed++;
			
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}

bool CFilter::polar()
//
// Desc:   Pretends the coordinates of the image are polar instead of cartesian and
//         plots the polar coordinates as cartesian.  No user option dialog.
// Pre:    m_buf != NULL; must call FilterInit() to initialize m_img and m_filt
// Post:   true:  m_filt contains the filtered image in matrix form
//         false: m_filt is undefined (user pressed cancel in a dialog)
// Source: [HOLZ88:48-49], [CLAE97]
//
{
	// Init Matrices m_img and m_filt
	FilterInit();

	// Setup and Call Progress Dialog
	CProgressDlg pdlg;
	pdlg.Create();
	unsigned int pixels = (unsigned int)(m_height * m_width * 0.01); // 1% of total pixels
	unsigned int pixelsProcessed = 0;
	bool canceled = FALSE;

	// Filter Algorithm /////////////////////////////////////

	int			 i, j;								// cartesian coordinates
	int			 x, y;								// converted cartesian coordinates
	int          k, l, m;
	
	double		 r, a;								// polar coordinates (radius, angle)
	unsigned int R  = (min(m_width, m_height))/2;   // maxium radius of polar coordinates

	int w2 = m_width  / 2;
	int h2 = m_height / 2;
	
	for (k = 0; k < (int)m_height; k++)
		for (l = 0; l < (int)m_width; l++)
			m_filt[k][l].color(255);

	for (i = 0; i < (int)m_height; i++)
	{
		if (pdlg.CheckCancelButton())
			if(AfxMessageBox("Are you sure you want to Cancel?", MB_YESNO)==IDYES)
			{
				canceled = true;
				break;
			}

		for (j = 0; j < (int)m_width; j++)
		{
			// move x, y so centerpixel is (0,0) -- for polar conversion
			x = i - h2;
			y = j - w2;
			
			// convert [x,y] to polar
			r = sqrt(x*x + y*y);
			a = atan2((float)x, y);

			// scaling
			x = int(r*m_height/R);
			y = int(a*m_width/6.2832);

			//move origin to top center (visually)
			x = m_height - x - 1;
			y += w2;

			k = i;
			l = j;
			
			//rotate filtered image 90-degrees clockwise
			m = l;
			l = k;
			k = m_height - m;

			//displace back to center origin
			k += (w2 - h2);
			l += (w2 - h2);

			// plot the pixel, if it exists
			if (l >= 0 && l < (int)m_width && k >= 0 && k < (int)m_height)
				if (x >= 0 && x < (int)m_height && y >= 0 && y < (int)m_width)
					m_filt[k][l].color(m_img[x][y].r, m_img[x][y].g, m_img[x][y].b);

			pixelsProcessed++;
			if (pixelsProcessed % pixels == 0)
				pdlg.StepIt();
		}
	}

	pdlg.DestroyWindow();
	return !canceled;
}
