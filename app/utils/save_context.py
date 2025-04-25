import os

def save_context(data: dict):

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
    context_path = os.path.join(data_dir, "context.txt")
    with open(context_path, "a", encoding="utf-8") as f:
        return f.write(formatted_text.strip())
