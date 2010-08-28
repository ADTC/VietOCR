/**
 * Copyright @ 2009 Quan Nguyen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package net.sourceforge.vietocr;

import java.awt.Rectangle;
import java.io.File;
import java.util.List;
import javax.imageio.IIOImage;

public class OCRImageEntity {
    /** input images */
    private List<IIOImage> oimages;
    /** input image File */
    private File imageFile;
    /** index of pages, such as in multi-page TIFF image */
    private int index;
    /** bounding rectangle */
    private Rectangle rect;
    /** Language code, which follows ISO 639-3 standard */
    private String lang;
    /** Horizontal Resolution */
    private int dpiX;
    /** Vertical Resolution */
    private int dpiY;

    /**
     * Constructor.
     * @param oimages a list of <code>IIOImage</code> objects
     * @param index
     * @param the bounding rectangle defines the region of the image to be recognized. A rectangle of zero dimension or <code>null</code> indicates the whole image.
     * @param lang language code, which follows ISO 639-3 standard
     */
    public OCRImageEntity(List<IIOImage> oimages, int index, String lang, Rectangle rect) {
        this.oimages = oimages;
        this.index = index;
        this.rect = rect;
        this.lang = lang;
    }

    /**
     * Constructor.
     * @param imageFile an image file
     * @param index
     * @param the bounding rectangle defines the region of the image to be recognized. A rectangle of zero dimension or <code>null</code> indicates the whole image.
     * @param lang language code, which follows ISO 639-3 standard
     */
    public OCRImageEntity(File imageFile, int index, String lang, Rectangle rect) {
        this.imageFile = imageFile;
        this.index = index;
        this.rect = rect;
        this.lang = lang;
    }

    /**
     * @return the list of oimages
     */
    public List<IIOImage> getOimages() {
        return oimages;
    }

    /**
     * @return the imageFile
     */
    public File getImageFile() {
        return imageFile;
    }

    /**
     * @return the ClonedImageFiles
     */
    public List<File> getClonedImageFiles() throws Exception {
        if (oimages != null) {
            if (dpiX != 0 && dpiY != 0) {
                return ImageIOHelper.createTiffFiles(oimages, index, dpiX, dpiY);
            } else {
                return ImageIOHelper.createTiffFiles(oimages, index);
            }
        } else {
            return ImageIOHelper.createTiffFiles(imageFile, index);
        }
    }

    /**
     * @return the index
     */
    public int getIndex() {
        return index;
    }

    /**
     * @return the bounding rectangle
     */
    public Rectangle getRect() {
        return rect;
    }

    /**
     * Sets screenshot mode.
     * @param mode true for resampling the input image; false for no manipulation of the image
     */
    public void setScreenshotMode(boolean mode) {
        dpiX = mode ? 300 : 0;
        dpiY = mode ? 300 : 0;
    }

    /**
     * Sets resolution (DPI).
     * @param dpiX horizontal resolution
     * @param dpiY vertical resolution
     */
    public void setResolution(int dpiX, int dpiY) {
        this.dpiX = dpiX;
        this.dpiY = dpiY;
    }

    /**
     * @return the language code
     */
    public String getLanguage() {
        return lang;
    }
}
