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

import java.io.File;
import java.util.List;
import javax.imageio.IIOImage;

public class OCRImageEntity {

    private List<IIOImage> originalImages;
    private File originalImageFile;
    private int index;
    /** Language code, which follows ISO 639-3 standard */
    private String lang;
    /** Horizontal Resolution */
    private int dpiX;
    /** Vertical Resolution */
    private int dpiY;

    /**
     * Constructor.
     * @param originalImages a list of <code>IIOImage</code> objects
     * @param index
     * @param lang language code, which follows ISO 639-3 standard
     */
    public OCRImageEntity(List<IIOImage> originalImages, int index, String lang) {
        this.originalImages = originalImages;
        this.index = index;
        this.lang = lang;
    }

    /**
     * Constructor.
     * @param originalImages an image file
     * @param index
     * @param lang language code, which follows ISO 639-3 standard
     */
    public OCRImageEntity(File originalImageFile, int index, String lang) {
        this.originalImageFile = originalImageFile;
        this.index = index;
        this.lang = lang;
    }

    /**
     * @return the originalImages
     */
    public List<IIOImage> getOriginalImages() {
        return originalImages;
    }

    /**
     * @return the originalImageFile
     */
    public File getOriginalImageFile() {
        return originalImageFile;
    }

    /**
     * @return the ClonedImageFiles
     */
    public List<File> getClonedImageFiles() throws Exception {
        if (originalImages != null) {
            if (dpiX != 0 && dpiY != 0) {
                return ImageIOHelper.createImageFiles(originalImages, index, dpiX, dpiY);
            } else {
                return ImageIOHelper.createImageFiles(originalImages, index);
            }
        } else {
            return ImageIOHelper.createImageFiles(originalImageFile, index);
        }
    }

    /**
     * @return the index
     */
    public int getIndex() {
        return index;
    }

    /**
     * Sets screenshot mode
     * @param mode true for resampling the input image; false for no manipulation of the image
     */
    public void setScreenshotMode(boolean mode) {
        dpiX = mode ? 300 : 0;
        dpiY = mode ? 300 : 0;
    }

    /**
     * Sets resolution
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
