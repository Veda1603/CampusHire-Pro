def extract_certifications(lines):

    certificates = []

    for line in lines:

        line = line.strip()

        if not line:
            continue

        if line.startswith(("•", "-", "*")):
            line = line[1:].strip()

        certificates.append(line)

    return certificates