// libAngioViewerCLR.h

#pragma once

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

typedef int(*cb_req_reCalc_t)(char*);

namespace libAngioViewerCLR {

	public ref class Viewer
	{
	public:
		static void open_viewer(String^ data_dir, String^ db_file_path, libAngioViewer::Entry::CbReqReCalc_t^ cbReqReCalc);
	};
}

__declspec(dllexport) void open_angio_viewer(char* data_dir, char* db_file_path, void* cb_req_reCalc)
{
	cb_req_reCalc_t cb_req_reCalc_clr = (cb_req_reCalc_t)cb_req_reCalc;

	String^ strDataDir = gcnew String(data_dir);
	String^ strDBFIlePath = gcnew String(db_file_path);
	libAngioViewer::Entry::CbReqReCalc_t^ pDelegate = (libAngioViewer::Entry::CbReqReCalc_t^)Marshal::GetDelegateForFunctionPointer((IntPtr)cb_req_reCalc_clr, libAngioViewer::Entry::CbReqReCalc_t::typeid);
	libAngioViewerCLR::Viewer::open_viewer(strDataDir, strDBFIlePath, pDelegate);
}