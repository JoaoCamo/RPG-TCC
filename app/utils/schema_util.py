import json
import os

def load_json_schema(filename: str):
    base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    data_dir = os.path.join(base_dir, "data")
    schema_path = os.path.join(data_dir, filename)
    with open(schema_path, "r", encoding="utf-8-sig") as f:
        return json.load(f)