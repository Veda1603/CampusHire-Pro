from flask import Flask

from utils.text_extractor import extract_text
from services.resume_service import parse_resume

app = Flask(__name__)


@app.route("/")
def home():
    return {"message": "CampusHire AI Running"}


@app.route("/resume")
def resume():

    text = extract_text("uploads/resume1.pdf")

    result = parse_resume(text)

    return result


if __name__ == "__main__":
    app.run(debug=True)