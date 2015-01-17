/**
 * Copyright @ 2012 Quan Nguyen
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
package net.sourceforge.vietocr;

import java.io.File;
import java.util.Arrays;
import java.util.List;
import javax.imageio.IIOImage;
//import net.sourceforge.tess4j.ITesseract.RenderedFormat;
import net.sourceforge.tess4j.Tesseract;
import net.sourceforge.vietocr.util.Utils;

/**
 * Invokes Tesseract OCR API through JNA-based Tess4J wrapper.<br />This could
 * be faster than the existing method since it feeds image data directly to the
 * OCR engine without creating intermediate working files (less I/O operations).
 * However, any exception from native code will result in hard crash of the
 * application.
 */
public class OCRImages extends OCR<IIOImage> {

    Tesseract instance;
    String datapath;

    public OCRImages(String datapath) {
        instance = Tesseract.getInstance();
        this.datapath = datapath;
        instance.setDatapath(datapath);
    }

    /**
     * Recognizes images.
     *
     * @param images as IIOImage
     * @return recognized text
     * @throws Exception
     */
    @Override
    public String recognizeText(List<IIOImage> images) throws Exception {
        instance.setLanguage(this.getLanguage());
        instance.setPageSegMode(Integer.parseInt(this.getPageSegMode()));
        instance.setHocr(this.getOutputFormat().equalsIgnoreCase("hocr"));
        String[] configs = {CONFIGS_FILE};
        instance.setConfigs(Arrays.asList(configs));
        controlParameters(instance);
        String text = instance.doOCR(images, rect);

        return text;
    }

    /**
     * Reads <code>tessdata/configs/tess_configvars</code> and
     * <code>setVariable</code> on Tesseract engine. This only works for
     * non-init parameters (@see
     * <a href="https://code.google.com/p/tesseract-ocr/wiki/ControlParams">ControlParams</a>).
     *
     * @param instance
     */
    void controlParameters(Tesseract instance) throws Exception {
        File configvarsFilePath = new File(datapath, "tessdata/configs/" + CONFIGVARS_FILE);
        if (!configvarsFilePath.exists()) {
            return;
        }

        String str = Utils.readTextFile(configvarsFilePath);

        for (String line : str.split("\n")) {
            if (!line.trim().startsWith("#")) {
                try {
                    String[] keyValuePair = line.trim().split("\\s+");
                    instance.setTessVariable(keyValuePair[0], keyValuePair[1]);
                } catch (Exception e) {
                    //ignore and continue on
                }
            }
        }
    }

    /**
     * Processes OCR for input file with specified output format.
     *
     * @param inputImage
     * @param outputFile
     * @throws Exception
     */
    @Override
    public void processPages(File inputImage, File outputFile) throws Exception {
//        instance.setLanguage(this.getLanguage());
//        instance.setPageSegMode(Integer.parseInt(this.getPageSegMode()));
//        List<RenderedFormat> formats = new ArrayList<RenderedFormat>();
//        formats.add(RenderedFormat.valueOf(this.getOutputFormat().toUpperCase()));
//        instance.createDocuments(inputImage.getPath(), Utils.stripExtension(outputFile.getPath()), formats);
    }
}
