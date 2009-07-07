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
package net.sourceforge.vietocr.postprocessing;

import java.io.File;
import java.util.*;
import org.apache.jetspeed.util.StringUtils;

/**
 *
 * @author Quan Nguyen (nguyenq@users.sf.net)
 */
public class Processor {

    public static String postProcess(String text, String langCode) {
        IPostProcessor processor = ProcessorFactory.createProcessor(ISO639.valueOf(langCode));
        return processor.postProcess(text);
    }

    public static String postProcess(String text, String langCode, File dangAmbigsPath) {
        // postprocessor
        StringBuffer strB = new StringBuffer(postProcess(text, langCode));

        // replace text based on entries read from a DangAmbigs.txt file
        Map<String, String> replaceRules = TextUtilities.loadMap(new File(dangAmbigsPath, langCode + ".DangAmbigs.txt"));
        Set<String> set = replaceRules.keySet();
        Iterator<String> iter = set.iterator();
        while (iter.hasNext()) {
            String key = iter.next();
            String value = replaceRules.get(key);
            strB = StringUtils.replaceAll(strB, key, value);
        }
        return strB.toString();
    }
}
