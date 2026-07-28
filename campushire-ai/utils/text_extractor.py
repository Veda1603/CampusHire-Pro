import os
import fitz
import pytesseract

from pdf2image import convert_from_path
from docx import Document


# Update this if your installation path is different
pytesseract.pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract.exe"

POPPLER_PATH = r"C:\Users\Admin\poppler-26.02.0\Library\bin"


def extract_text(file_path):

    extension = os.path.splitext(file_path)[1].lower()

    if extension == ".pdf":
        return extract_pdf(file_path)

    elif extension == ".docx":
        return extract_docx(file_path)

    else:
        raise Exception("Unsupported file format")


def extract_pdf(file_path):

    text = ""

    pdf = fitz.open(file_path)

    for page in pdf:
        text += page.get_text("text")

    pdf.close()

    # If normal extraction works, return it
    if text.strip():
        print("✓ Text extracted using PyMuPDF")
        return text

    print("No text found. Switching to OCR...")

    return extract_using_ocr(file_path)


def extract_using_ocr(file_path):
    text = ""

    images = convert_from_path(
        file_path,
        poppler_path=POPPLER_PATH
    )

    for image in images:
        page_text = pytesseract.image_to_string(image)
        print("OCR Output:\n", page_text[:500])   # Debug
        text += page_text

    return text


def extract_docx(file_path):

    document = Document(file_path)

    text = ""

    for paragraph in document.paragraphs:
        text += paragraph.text + "\n"

    return text