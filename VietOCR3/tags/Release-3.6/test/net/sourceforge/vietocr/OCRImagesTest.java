package net.sourceforge.vietocr;

import java.io.File;
import java.util.List;
import javax.imageio.IIOImage;
//import net.sourceforge.tess4j.util.ImageIOHelper;
import org.junit.*;
import static org.junit.Assert.*;

public class OCRImagesTest {

    static final boolean WINDOWS = System.getProperty("os.name").toLowerCase().startsWith("windows");
    String tessPath;
    String datapath;
    String lang = "vie";
    OCRImageEntity entity;

    public OCRImagesTest() {
        tessPath = WINDOWS ? new File(System.getProperty("user.dir"), Gui.TESSERACT_PATH).getPath() : "/usr/local/bin";
        datapath = WINDOWS ? tessPath : "/usr/share/tesseract-ocr/";
        File selectedFile = new File("samples/vietsample1.tif");
        try {
            List<IIOImage> iioImageList = ImageIOHelper.getIIOImageList(selectedFile);
            entity = new OCRImageEntity(iioImageList, selectedFile.getPath(), -1, null, "vie");
        } catch (Exception e) {
        }
    }

    @BeforeClass
    public static void setUpClass() throws Exception {
    }

    @AfterClass
    public static void tearDownClass() throws Exception {
    }

    @Before
    public void setUp() {
    }

    @After
    public void tearDown() {
    }

    /**
     * Test of recognizeText method, of class OCRImages.
     *
     * @throws java.lang.Exception
     */
    @Test
    public void testRecognizeText() throws Exception {
        System.out.println("recognizeText with Tesseract API");
        OCR<IIOImage> instance = new OCRImages(tessPath);
        instance.setDatapath(datapath);
        instance.setLanguage(lang);
        String expResult = "Tôi từ chinh chiến cũng ra đi";
        String result = instance.recognizeText(entity.getSelectedOimages(), entity.getInputfilename());
        System.out.println(result);
        assertTrue(result.toLowerCase().contains(expResult.toLowerCase()));
    }
}
