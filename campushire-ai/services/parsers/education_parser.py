import re


# =====================================================
# MAIN EDUCATION PARSER
# =====================================================

def extract_education(lines):

    education = []

    lines = [
        normalize_ocr_line(x)
        for x in lines
        if normalize_ocr_line(x)
    ]


    current = None


    for line in lines:


        # Stop education section
        if is_next_section(line):
            break



        # Year line
        years = extract_years(line)


        # Institution line
        if is_institution(line):

            if current and current["degree"]:
                education.append(current)


            current = create_entry()

            current["institution"] = clean_institution(line)

            current["location"] = extract_location(line)


            if years:

                current["start_year"] = years[0]
                current["end_year"] = years[1]


            continue



        # Degree line
        if is_degree(line):

            if current is None:

                current = create_entry()


            current["degree"] = clean_degree(line)


            current["specialization"] = (
                extract_specialization(line)
            )


            current["cgpa"] = extract_cgpa(line)


            current["percentage"] = (
                extract_percentage(line)
            )


            single = extract_single_year(line)

            if single:
                current["end_year"] = single


            continue



        # Separate year line
        if years and current:

            current["start_year"] = years[0]
            current["end_year"] = years[1]

            continue



        # Separate score line
        if current:

            cgpa = extract_cgpa(line)

            if cgpa:
                current["cgpa"] = cgpa


            percentage = extract_percentage(line)

            if percentage:
                current["percentage"] = percentage



    if current and (
        current["degree"]
        or current["institution"]
    ):

        education.append(current)



    return remove_duplicates(education)



# =====================================================
# OBJECT
# =====================================================

def create_entry():

    return {

        "institution": "",
        "location": "",

        "degree": "",

        "specialization": "",

        "start_year": "",
        "end_year": "",

        "cgpa": "",
        "percentage": ""

    }



# =====================================================
# OCR CLEAN
# =====================================================

def normalize_ocr_line(line):

    line = line.strip()


    line = re.sub(
        r"^[•●▪◦\-*]+\s*",
        "",
        line
    )


    line = re.sub(
        r"\s+",
        " ",
        line
    )


    fixes = {

        "G . V .": "G.V.",
        "G . V": "G.V.",

        "C G P A": "CGPA",

        "EDUCA TION": "EDUCATION",

        "EDUCAT ION": "EDUCATION",

        "10 th": "10th",

        "B E": "B.E",

        "B . E .": "B.E"

    }


    for a,b in fixes.items():

        line = line.replace(a,b)


    return line.strip()



# =====================================================
# SECTION STOP
# =====================================================

def is_next_section(line):

    sections = [

        "PERSONAL DETAILS",
        "SKILLS",
        "PROJECTS",
        "CERTIFICATION",
        "INTERNSHIP",
        "EXPERIENCE"

    ]


    return line.upper() in sections



# =====================================================
# DEGREE DETECTOR
# =====================================================

def is_degree(text):

    patterns = [

        r"\bB\.?E\b",

        r"\bB\.?Tech\b",

        r"\bBachelor\b",

        r"\bDiploma\b",

        r"\bM\.?Tech\b",

        r"\bMCA\b",

        r"\bMBA\b",

        r"\bBCA\b",

        r"\b10th\b",

        r"\bSSC\b",

        r"\bHSC\b",

        r"Secondary School"

    ]


    return any(
        re.search(
            p,
            text,
            re.I
        )
        for p in patterns
    )



# =====================================================
# INSTITUTION DETECTOR
# =====================================================

def is_institution(text):

    keywords = [

        "Institute",
        "Institute",
        "College",
        "University",
        "Polytechnic",
        "Polytechnique",
        "School",
        "Vidyalaya",
        "Vidyamandir",
        "Academy"

    ]


    return any(
        k.lower() in text.lower()
        for k in keywords
    )



# =====================================================
# YEAR
# =====================================================

def extract_years(text):

    match = re.search(
        r"(19\d{2}|20\d{2})\s*[-–]\s*(19\d{2}|20\d{2})",
        text
    )


    if match:

        return [
            match.group(1),
            match.group(2)
        ]

    return []



def extract_single_year(text):

    match = re.search(
        r"\b(19\d{2}|20\d{2})\b",
        text
    )

    return match.group(1) if match else ""



# =====================================================
# DEGREE CLEAN
# =====================================================

def clean_degree(text):

    text = re.sub(
        r"(May|March|June|April|Dec|Jan|Feb)\s*-\s*\d{4}",
        "",
        text,
        flags=re.I
    )


    return text.strip()



# =====================================================
# LOCATION
# =====================================================

def extract_location(text):

    locations = [

        "Karjat",
        "Ambernath",
        "Mumbai",
        "Pune",
        "Nashik",
        "Thane",
        "India"

    ]


    found=[]


    for loc in locations:

        if loc.lower() in text.lower():

            found.append(loc)


    return ", ".join(found)



# =====================================================
# CLEAN INSTITUTION
# =====================================================

def clean_institution(text):

    text = re.sub(
        r"\d{4}\s*[-–]\s*\d{4}",
        "",
        text
    )

    return text.strip()



# =====================================================
# CGPA
# =====================================================

def extract_cgpa(text):

    if "percentage" in text.lower():
        return ""


    match = re.search(

        r"(?:CGPA).*?(\d+\.\d+)",

        text,

        re.I

    )


    if match:

        return match.group(1)


    match = re.search(
        r"(\d\.\d+)\s*/\s*10",
        text
    )


    return match.group(1) if match else ""



# =====================================================
# PERCENTAGE
# =====================================================

def extract_percentage(text):

    match = re.search(

        r"(?:Percentage).*?(\d+\.\d+)",

        text,

        re.I

    )


    if match:

        return match.group(1)


    match = re.search(

        r"(\d+\.\d+)\s*/\s*100",

        text

    )


    return match.group(1) if match else ""



# =====================================================
# SPECIALIZATION
# =====================================================

def extract_specialization(text):

    match = re.search(
        r"\((.*?)\)",
        text
    )


    if match:

        value = match.group(1)

        if "aggregate" not in value.lower():

            return value.strip()


    return ""



# =====================================================
# DUPLICATE REMOVE
# =====================================================

def remove_duplicates(data):

    result=[]

    seen=set()


    for item in data:

        key=(

            item["institution"],
            item["degree"]

        )


        if key not in seen:

            result.append(item)

            seen.add(key)


    return result