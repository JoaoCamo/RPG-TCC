import os


def save_context(data: dict, file: str):
    def format_dict(data, current_indent=0):
        lines = []
        spacing = "    " * current_indent

        for key in data.keys():
            value = data.get(key)
            if isinstance(value, dict):
                lines.append(f"\n{spacing}{key.capitalize()}:")
                lines.append(format_dict(value, current_indent + 1))
            else:
                lines.append(f"\n{spacing}{key.capitalize()}: {value}")
        return "\n".join(lines)

    formatted_text = format_dict(data)

    base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    data_dir = os.path.join(base_dir, "data")
    context_path = os.path.join(data_dir, file)
    with open(context_path, "a", encoding="utf-8") as f:
        return f.write(formatted_text.strip())


def load_context(file: str):
    base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    data_dir = os.path.join(base_dir, "data")
    context_path = os.path.join(data_dir, file)
    try:
        with open(context_path, "r", encoding="utf-8") as f:
            return f.read().strip()
    except FileNotFoundError:
        return ""

def clear_context(file: str):
    base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    data_dir = os.path.join(base_dir, "data")
    context_path = os.path.join(data_dir, file)

    os.makedirs(data_dir, exist_ok=True)

    with open(context_path, "w", encoding="utf-8") as f:
        f.write("")

    return True