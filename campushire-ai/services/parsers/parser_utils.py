import re


# ---------------------------------
# Degree Detection
# ---------------------------------

DEGREE_KEYWORDS = [
    "B.E",
    "BE",
    "B.Tech",
    "Bachelor",
    "M.E",
    "M.Tech",
    "Master",
    "MCA",
    "BCA",
    "Diploma",
    "SSC",
    "HSC",
    "10th",
    "12th"
]


def detect_degree(text):

    for degree in DEGREE_KEYWORDS:

        if re.search(
            r"\b" + re.escape(degree) + r"\b",
            text,
            re.I
        ):
            return degree

    return ""



# ---------------------------------
# Specialization Detection
# ---------------------------------

SPECIALIZATION_KEYWORDS = [
    "Artificial Intelligence and Machine Learning",
    "Computer Science Engineering",
    "Computer Engineering",
    "Information Technology",
    "Artificial Intelligence",
    "Data Science",
    "Cyber Security"
]


def detect_specialization(text):

    for item in SPECIALIZATION_KEYWORDS:

        if item.lower() in text.lower():

            return item

    # Check bracket content
    match = re.search(
        r"\((.*?)\)",
        text
    )

    if match:
        return match.group(1)

    return ""



# ---------------------------------
# Year Detection
# ---------------------------------

def detect_years(text):

    match = re.search(
        r"\b(19\d{2}|20\d{2})\s*(?:-|–|to)\s*(19\d{2}|20\d{2})\b",
        text,
        re.I
    )

    if match:

        return {
            "start_year": match.group(1),
            "end_year": match.group(2)
        }


    match = re.search(
        r"\b(19\d{2}|20\d{2})\s*(?:-|to)\s*(Present|Current)\b",
        text,
        re.I
    )

    if match:

        return {
            "start_year": match.group(1),
            "end_year": "Present"
        }


    return {
        "start_year": "",
        "end_year": ""
    }

# ---------------------------------
# CGPA Detection
# ---------------------------------

def detect_cgpa(text):

    patterns = [

        r"CGPA\s*[:\-]?\s*(\d+\.\d+)",

        r"GPA\s*[:\-]?\s*(\d+\.\d+)",

        r"(\d+\.\d+)\s*/\s*10\b"

    ]


    for pattern in patterns:

        match = re.search(
            pattern,
            text,
            re.I
        )

        if match:

            return match.group(1)


    return ""



# ---------------------------------
# Percentage Detection
# ---------------------------------

def detect_percentage(text):

    patterns = [

        r"Percentage\s*[:\-]?\s*(\d+\.\d+)",

        r"(\d+\.\d+)\s*/\s*100",

        r"(\d+\.\d+)\s*%",

        r"(\d+)\s*%"

    ]


    for pattern in patterns:

        match = re.search(
            pattern,
            text,
            re.I
        )

        if match:

            return match.group(1)


    return ""



# ---------------------------------
# Institution Detection
# ---------------------------------

INSTITUTION_WORDS = [

    "Institute",
    "Institution",
    "College",
    "University",
    "Polytechnic",
    "School",
    "Vidyalaya",
    "Vidyamandir",
    "Academy"

]


def detect_institution(text):

    for word in INSTITUTION_WORDS:

        if word.lower() in text.lower():

            return True


    return False



# ---------------------------------
# Location Detection
# ---------------------------------

def detect_location(text):

    parts = [
        x.strip()
        for x in text.split(",")
    ]


    if len(parts) > 1:

        return ", ".join(parts[1:])


    return ""



# ---------------------------------
# Clean line
# ---------------------------------

def clean_line(line):

    line = line.strip()

    line = re.sub(
        r"^[•●▪\-]\s*",
        "",
        line
    )

    return line.strip()