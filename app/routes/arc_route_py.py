from flask import Blueprint, request, jsonify
from app.services.arc_service import generate_arc

arc_blueprint = Blueprint("arc", __name__)

@arc_blueprint.route("/", methods=["POST"])
def arc():
    data = request.get_json()
    selected_model = data.get("selectedModel")
    last_action = data.get("lastPlayerAction")
    current_arc = data.get("currentArc")
    is_ending = data.get("isEnding")
    arc = generate_arc(selected_model, last_action, current_arc, is_ending)
    return jsonify(arc)