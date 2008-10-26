/**
 * Copyright @ 2008 Quan Nguyen
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

import java.io.*;
import java.util.*;

/**
 *
 * @author Quan Nguyen (nguyenq@users.sf.net)
 */
public class OCR {
    private final String LANG_OPTION = "-l";
    private final String EOL = System.getProperty("line.separator");
    
    private String tessPath;
    
    /** Creates a new instance of OCR */
    public OCR(String tessPath) {
        this.tessPath = tessPath;
    }
    
    String recognizeText(File imageFile, int index, String imageFormat, String lang) throws Exception {
        ArrayList<File> tempImages = ImageIOHelper.createImages(imageFile, index, imageFormat);
        
        File outputFile = new File(imageFile.getParentFile(), "output");
        StringBuffer strB = new StringBuffer();
        
        List<String> cmd = new ArrayList<String>();
        cmd.add(tessPath + "/tesseract");
        cmd.add(""); // placeholder for inputfile
        cmd.add(outputFile.getName());
        cmd.add(LANG_OPTION);
        cmd.add(lang);
            
        ProcessBuilder pb = new ProcessBuilder();
        pb.directory(imageFile.getParentFile());
            
        for (File tempImage : tempImages) {
            // actual output file will be "output.txt"
//            ProcessBuilder pb = new ProcessBuilder(tessPath + "/tesseract", tempImage.getAbsolutePath(), outputFile.getAbsolutePath(), LANG_OPTION, lang);            
            
            cmd.set(1, tempImage.getName());
            pb.command(cmd);
            pb.redirectErrorStream(true);
            Process process = pb.start();
//            Process process = Runtime.getRuntime().exec(cmd.toArray(new String[0]));
            
            int w = process.waitFor();
            System.out.println("Exit value = " + w);
            
            // delete temp working files
            tempImage.delete();
            
            if (w == 0) {
                BufferedReader in = new BufferedReader(new InputStreamReader(new FileInputStream(outputFile.getAbsolutePath() + ".txt"), "UTF-8"));
                
                String str;
                
                while ((str = in.readLine()) != null) {
                    strB.append(str).append(EOL);
                }
                in.close();
            } else {
                String msg;
                switch (w) {
                    case 1:
                        msg = "Errors accessing files. There may be spaces in your image's filename.";
                        break;
                    case 29:
                        msg = "Cannot recognize the image or its selected region.";
                        break;
                    case 31:
                        msg = "Unsupported image format.";
                        break;                        
                    default:
                        msg = "Errors occurred.";
                }
                for (File image : tempImages) {
                    image.delete();
                }
                throw new RuntimeException(msg);
            }
            
        }
        new File(outputFile.getAbsolutePath() + ".txt").delete();
        return strB.toString();
    }
}
