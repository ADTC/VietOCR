package net.sourceforge.vietocr.wia;

import java.io.File;
import com.jacob.activeX.ActiveXComponent;
import com.jacob.com.*;

/**
 *
 * @author Quan Nguyen (nguyenq@users.sf.net)
 */
public final class WiaScannerAdapter {

    private ActiveXComponent _wiaManager; // CommonDialogClass
    private boolean _disposed; // indicates if Dispose has been called
    private final Variant True = new Variant(true);
    private final Variant False = new Variant(false);

    public WiaScannerAdapter() {
    }

    Dispatch getWiaManager() {
        return _wiaManager;
    }

    void setWiaManager(ActiveXComponent value) {
        _wiaManager = value;
    }

    public File ScanImage(FormatID outputFormat, String fileName) throws Exception {
        if (outputFormat == null) {
            throw new IllegalArgumentException("outputFormat");
        }
      
        Dispatch imageObject = null; // "WIA.ImageFile"

        try {
            if (_wiaManager == null) {
                _wiaManager = new ActiveXComponent("WIA.CommonDialog");
            }

            imageObject = Dispatch.callN(_wiaManager, "ShowAcquireImage", new Variant[] {
                new Variant(WiaDeviceType.ScannerDeviceType), new Variant(WiaImageIntent.GrayscaleIntent),
                new Variant(WiaImageBias.MaximizeQuality), new Variant(outputFormat.getValue()),
                False, True, True} ).getDispatch();

            Dispatch.call(imageObject, "SaveFile", fileName);

            return new File(fileName);
        } catch (Exception ex) {
            String message = "Error scanning image";
            throw new WiaOperationException(message, ex);
        } finally {
            if (imageObject != null) {
                imageObject = null;
                imageObject.safeRelease();
//                ComThread.Release();
            }
        }
    }

    public void Dispose() {
        Dispose(true);
    }

    private void Dispose(boolean disposing) {
        if (!_disposed) {
            if (disposing) {
                // no managed resources to cleanup
            }
            // cleanup unmanaged resources
            if (_wiaManager != null) {
                _wiaManager = null;
                _wiaManager.safeRelease();
            }

            _disposed = true;
        }
    }
}
