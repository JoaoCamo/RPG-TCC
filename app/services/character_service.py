from openai import OpenAI

from app.utils.character_util import character_schema, character_system, character_user

client = OpenAI()


def generate_character():
    response = client.responses.create(
        model="gpt-4o-2024-08-06",
        input=[
            {"role": "system", "content": character_system},
            {"role": "user", "content": character_user},
        ],
        text=character_schema,
    )
    return response
