from openai import OpenAI

client = OpenAI()


def generate_character():
    response = client.responses.create(
        model="gpt-4o-2024-08-06",
        input=[{"role": "system", "content": ""}, {"role": "user", "content": ""}],
    )
    return response
