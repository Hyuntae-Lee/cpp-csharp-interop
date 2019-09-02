// This is the main DLL file.

#include "stdafx.h"
#include "libAngioViewerCLR.h"

using namespace libAngioViewerCLR;

void Viewer::open_viewer(String^ data_dir, String^ db_file_path, libAngioViewer::Entry::CbReqReCalc_t^ cbReqReCalc) {
	libAngioViewer::Entry::open_viewer(data_dir, db_file_path, cbReqReCalc);
}
